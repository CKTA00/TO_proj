using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingApplication.ParkingSystem;
using ParkingApplication.DeviceInterface;
using ParkingApplication.Premium;

namespace ParkingApplication.Devices
{
    class ExitParkingDevice : GateDevice, ICodeScannerObserver
    {
        public ExitParkingDevice(ISimpleDialog initDisplay, IGateAPI machine, TicketDatabase normalTicketsDB, TicketDatabase handicappedTicketsDB, PremiumDatabase premiumDB) 
            : base(initDisplay, machine, normalTicketsDB, handicappedTicketsDB, premiumDB)
        {

        }

        public override void Main()
        {
            display.ShowMessage("Zeskanuj kod kreskowy z biletu lub użyj karty stałego klienta.");
            //display.ShowMessage("Naciśnij przycisk aby wyjechać bez biletu (75 PLN).");
        }

        public void CodeScanned(string code)
        {
            Ticket ticket;
            TicketDatabase sourceDB;
            try
            {
                ticket = normalTicketsDB.TryEvaluateTicket(code);
                sourceDB = normalTicketsDB;
            }
            catch(InvalidTicketCodeException)
            {
                try
                {
                    ticket = handicappedTicketsDB.TryEvaluateTicket(code);
                    sourceDB = handicappedTicketsDB;
                }
                catch (InvalidTicketCodeException e)
                {
                    display.ShowMessage("Twój bilet jest nieważny! e: " + e.Message);
                    return;
                }        
            }

            if(!ticket.IsPaid)
            {
                display.ShowMessage("Zapłać za swój bilet!");
                return;
            }
           
            if (ticket.PaymentTime.AddMinutes(15) < DateTime.Now)
            {
                display.ShowMessage("Wykryto postój dłuższy niż zapłacono. Wróć się do automatu i zapłać za dodatkowy czas.");
                ticket.Underpaid();
                return;
            }

            sourceDB.DestroyTicket(ticket.Code);
            display.ShowMessage("Dziękujemy!");
            gate.OpenGate();
        }

        public override void CardSwiped(string code)
        {
            display.ShowMessage("Odczytywanie danych z karty. Czekaj...");
            PremiumUser u;
            try
            {
                u = premiumDB.FindUserByCode(code);
            }
            catch (InvalidPremiumUserCodeException e)
            {
                display.ShowMessage("Nie znaleziono takiej karty w bazie danych e: " + e.Message);
                return;
            }

            if (u.ExpiryDate <= DateTime.Now)
            {
                display.ShowMessage("Ważność twojej karty wygasła! Doładuj ją w naszym automacie.");
                return;
            }

            u.RemoveTicket();
            display.ShowMessage("Dziękujemy!");
            gate.OpenGate();
        }

        public override void ButtonPressed(ButtonKey key)
        {
            
        }
    }
}
