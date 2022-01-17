using System;
using System.Collections.Generic;
using ParkingApplication.ParkingSystem;
using ParkingApplication.DeviceInterface;

namespace ParkingApplication.Devices
{
    class EntranceParkingDevice : GateDevice
    {
        IPrinterAPI ticketPrinter;

        public EntranceParkingDevice(ISimpleDialog display, IGateAPI gate, IPrinterAPI ticketPrinter, TicketDatabase normalTicketsDB,TicketDatabase handicappedTicketsDB)
            :base(display, gate, normalTicketsDB, handicappedTicketsDB)
        {
            this.ticketPrinter = ticketPrinter;
        }

        public override void Main()
        {
            display.ShowMessage("Witaj! Wciśnij przycisk lub pryłóż kartę stałego klienta.");
        }

        public override void ButtonPressed(ButtonKey key)
        {
            if (key == ButtonKey.ACCEPT_BUTTON)
            {
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
            // TODO: chcek if id in premium database
            // yes -> open gate
            // no -> display.ShowMessage("Twoja karta nie mogła być odczytna. Spróbuj jeszcze raz!");
        }

        private void SpecialButtonPressed()
        {
            Ticket ticket;
            try
            {
                ticket = normalTicketsDB.TryAddTicket();
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
