using System;
using System.Collections.Generic;
using ParkingApplication.ParkingSystem;
using ParkingApplication.UserInterface;

namespace ParkingApplication.Devices
{
    class EntranceParkingMachine : GateDevice
    {
        public EntranceParkingMachine(ISimpleDialog display, IMachineAPI machine, TicketDatabase normalTicketsDB,TicketDatabase handicappedTicketsDB)
            :base(display, machine, normalTicketsDB, handicappedTicketsDB)
        {

        }

        public override void Main()
        {
            display.ShowMessage("Witaj! Wciśnij przycisk lub pryłóż kartę stałego klienta.");
        }


        public override void AcceptButtonPressed()
        {
            Ticket ticket;
            try
            {
                ticket = normalTicketsDB.TryAddTicket();
            }
            catch(NoPlaceLeftException e)
            {
                display.ShowMessage("Brak miejsc parkingowych! e:"+ e.Message);
                return;
            }
            machine.PrintTicket(ticket);
            machine.OpenGate();
        }

        public void CheckCard(int data)
        {
            display.ShowMessage("Odczytywanie danych z karty. Czekaj...");
            // TODO: chcek if id in premium database
            // yes -> open gate
            // no -> display.ShowMessage("Twoja karta nie mogła być odczytna. Spróbuj jeszcze raz!");
        }

    }
}
