using System;
using ParkingApplication.ParkingSystem;
using ParkingApplication.DeviceInterface;
using ParkingApplication.Premium;

namespace ParkingApplication.Devices
{
    class EntranceParkingDevice : GateDevice
    {
        IPrinterAPI ticketPrinter;
        bool handicapPremiumNormalTicket;
        PremiumUser tempUser;

        public EntranceParkingDevice(ISimpleDialog display, IGateAPI gate, IPrinterAPI ticketPrinter, TicketDatabase normalTicketsDB,TicketDatabase handicappedTicketsDB,PremiumDatabase premiumDB)
            :base(display, gate, normalTicketsDB, handicappedTicketsDB, premiumDB)
        {
            this.ticketPrinter = ticketPrinter;
        }

        public override void Main()
        {
            display.ShowMessage("Witaj! Wciśnij przycisk lub pryłóż kartę stałego klienta.");
            handicapPremiumNormalTicket = false;
        }

        public override void ButtonPressed(ButtonKey key)
        {
            if (key == ButtonKey.ACCEPT_BUTTON)
            {
                if (handicapPremiumNormalTicket)
                    HandicapPremiumNormalTicket();
                else
                    AcceptButtonPressed();
            }
            else if (key == ButtonKey.SPECIAL_BUTTON)
            {
                SpecialButtonPressed();
            }
        }

        private void AcceptButtonPressed()
        {
            Ticket ticket;
            try
            {
                ticket = normalTicketsDB.TryAddTicket();
            }
            catch(NoPlaceLeftException e)
            {
                display.ShowMessage("Brak zwykłych miejsc parkingowych! e:"+ e.Message);
                return;
            }
            ticketPrinter.PrintTicket(ticket);
            gate.OpenGate();
        }

        public override void CardSwiped(string data)
        {
            display.ShowMessage("Odczytywanie danych z karty. Czekaj...");
            PremiumUser u;
            try
            {
                u = premiumDB.FindUserByCode(data);
            }
            catch(InvalidPremiumUserCodeException e)
            {
                display.ShowMessage("Nie znaleziono takiej karty w bazie danych e: "+e.Message);
                return;
            }

            if(u.ExpiryDate <= DateTime.Now)
            {
                display.ShowMessage("Ważność twojej karty wygasła! Doładuj ją w naszym automacie.");
                return;
            }

            Ticket virtualTicket;
            if(u.IsHandicapped)
            {
                try
                {
                    virtualTicket = handicappedTicketsDB.TryAddTicket();
                }
                catch (NoPlaceLeftException e)
                {
                    display.ShowMessage("Brak miejsc parkingowych dla osób niepełnosprawnych! e:" + e.Message);
                    display.ShowMessage("Kliknij przycisk ACCEPT jeśli chcesz wjechać mimo to.");
                    handicapPremiumNormalTicket = true;
                    tempUser = u;
                    return;
                }
                AddVirtualTicket(virtualTicket,u);
            }
            else
            {
                try
                {
                    virtualTicket = normalTicketsDB.TryAddTicket();
                }
                catch (NoPlaceLeftException e)
                {
                    display.ShowMessage("Brak zwykłych miejsc parkingowych! e:" + e.Message);
                    return;
                }
                AddVirtualTicket(virtualTicket,u);
            }
        }

        private void AddVirtualTicket(Ticket virtualTicket, PremiumUser user)
        {
            try
            {
                user.AddTicket(virtualTicket);
            }
            catch(PremiumCurrentlyUsedException e)
            {
                display.ShowMessage("Niestety twoja karta jest już w użyciu! e: "+e.Message);
                return;
            }
            display.ShowMessage("Witaj!");
            gate.OpenGate();
        }

        private void HandicapPremiumNormalTicket()
        {
            Ticket virtualTicket;
            try
            {
                virtualTicket = normalTicketsDB.TryAddTicket();
                AddVirtualTicket(virtualTicket, tempUser);
            }
            catch (NoPlaceLeftException e)
            {
                display.ShowMessage("Brak zwykłych miejsc parkingowych! e:" + e.Message);
            }
            handicapPremiumNormalTicket = false;
            tempUser = null;
        }

        private void SpecialButtonPressed()
        {
            Ticket ticket;
            try
            {
                ticket = handicappedTicketsDB.TryAddTicket();
            }
            catch (NoPlaceLeftException e)
            {
                display.ShowMessage("Brak miejsc parkingowych dla osób niepełnosprawnych! e:" + e.Message);
                display.ShowMessage("Kliknij przycisk ACCEPT jeśli chcesz wjechać mimo to.");
                return;
            }
            ticketPrinter.PrintTicket(ticket);
            gate.OpenGate();
        }
    }
}
