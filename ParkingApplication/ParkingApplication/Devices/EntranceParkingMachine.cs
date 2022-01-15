using System;
using System.Collections.Generic;
using ParkingApplication.UserInterface;

namespace ParkingApplication.Devices
{
    class EntranceParkingMachine : Device
    {
        public EntranceParkingMachine(ISimpleDialog ui):base(ui)
        {
            //nothing?
        }

        public override void Main()
        {
            display.ShowMessage("Witaj! Wciśnij przycisk lub pryłóż kartę stałego klienta.");
        }


        public override void AcceptButtonPressed()
        {
            // TODO: generate code
            display.ShowMessage("[Wysuwa bilet z kodem kreskowym " + "temp" + "]"); // TODO: replace with action function (not message)
            display.ShowMessage("Zachowaj ten bilet do wyjazdu z parkingu.");
            // TODO: open gate
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
