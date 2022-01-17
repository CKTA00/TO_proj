using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingApplication.ParkingSystem;

namespace ParkingApplication.UserInterface
{
    class ConsoleMachineAPI : ISimpleDialog, IMachineAPI
    {
        ConsoleDisplay con;

        public ConsoleMachineAPI()
        {
            // current implementation uses console. Might use WPF desktop window.
            con = ConsoleDisplay.GetInstance();
        }

        public void OpenGate()
        {
            con.ShowMessage("[Brama otwarła się]");
            //symulacja sensora
            con.ShowMessage("Naciśnij enter aby zasymulować wjechanie pojazdu na teren parkingu oraz wykrycie przez sensor tego faktu.");
            con.ReadString();
            CloseGate();
        }

        private void CloseGate()
        {
            con.ShowMessage("[Brama zamkneła się]");
        }

        public void PrintTicket(Ticket ticket)
        {
            con.ShowMessage("[Wysuwa bilet z kodem kreskowym " + ticket.Code + "]"); // TODO: replace with action function (not message)
            ShowMessage("Zachowaj ten bilet do wyjazdu z parkingu.");
        }

        public string ReadString()
        {
            con.ShowMessage("[Klawiatura Automatu]");
            return con.ReadString();
        }

        public void ShowMessage(string msg)
        {
            con.ShowMessage("[Automat] " + msg);
        }
    }
}
