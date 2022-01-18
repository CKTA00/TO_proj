using System;
using ParkingApplication.Devices;
using ParkingApplication.DeviceInterface;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingApplication.ParkingSystem;
using ParkingApplication.Util;
using ParkingApplication.Premium;

namespace ParkingApplication
{
    class ApplicationClient
    {
        static ConsoleMachineAPI machine;
        static ICodeGenerator generator;
        static ISimpleDialog con;
        static DeviceFactory app;
        static TicketDatabase normalTicketDB;
        static TicketDatabase handicappedTicketDB;
        static void Main(string[] args)
        {
            con = ConsoleDisplay.GetInstance();
            machine = new ConsoleMachineAPI();
            generator = new GUIDGenerator();

            normalTicketDB = new TicketDatabase(generator, 40);
            handicappedTicketDB = new TicketDatabase(generator, 5);
            PremiumDatabase premiumDatabase = new PremiumDatabase(generator);

            app = DeviceFactory.GetInstance();
            app.Buttons = machine;
            app.CardReaader = machine;
            app.Gate = machine;
            app.HandicappedTicketDB = handicappedTicketDB;
            app.NormalTicketDB = normalTicketDB;
            app.PremiumDatabase = premiumDatabase;
            app.Scanner = machine;
            app.TicketPrinter = machine;
            app.Ui = machine;
            app.Run();

            ShowSimulationMenu();
        }

        static void ShowSimulationMenu()
        {
            while (true)
            {
                con.ShowMessage("Zasymuluj działanie parkomatu. Wpisz jeden z poniższych usecaseów:");
                con.ShowMessage(">drive in");
                con.ShowMessage("\tSymuluje wjazd nowego użytkownika na parking.");
                con.ShowMessage(">drive out");
                con.ShowMessage("\tSymuluje wyjazd losowego użytkownika z parkingu.");
                con.ShowMessage(">register");
                con.ShowMessage("\tSymuluje rejestracje noweg stałego użytkownika (premium)");
                con.ShowMessage(">exit");
                con.ShowMessage("\tWyjdź.");
                string command = con.ReadString();
                if (command == "drive in") //might use chain of responsibility if enough time
                {
                    DriveIn();
                }
                else if (command == "drive out")
                {
                    
                }
                else if (command == "register")
                {

                }
                else if (command == "exit")
                {
                    break;
                }
                else
                {
                    con.ShowMessage("Nieprawidłowe polecenie!");
                    con.ShowMessage("");
                    con.ShowMessage("");
                }
            }
        }

        static void DriveIn()
        {
            EntranceParkingDevice device = app.GetEntranceDevices()[0];
            device.Main();

            while (true)
            {
                con.ShowMessage(">press accept");
                con.ShowMessage("\tNaciśnij główny przycisk.");
                con.ShowMessage(">press special");
                con.ShowMessage("\tNaciśnij przycisk specialny (z ikoną człowieka na wózku).");
                con.ShowMessage(">card");
                con.ShowMessage("\tPrzyłóż kartę.");
                con.ShowMessage(">exit");
                con.ShowMessage("\tZawróć.");
                string command = con.ReadString();
                if (command == "press accept")
                {
                    machine.AnnounceButtonPressed(ButtonKey.ACCEPT_BUTTON, device);
                }
                else if (command == "press special")
                {
                    machine.AnnounceButtonPressed(ButtonKey.SPECIAL_BUTTON, device);
                }
                else if (command == "card")
                {
                    SwipeCard(device);
                }
                else if (command == "exit")
                {
                    break;
                }
                else
                {
                    IncorrectCommand();
                }
            }
        }

        private static void SwipeCard(EntranceParkingDevice device)
        {
            string cardCode;

            con.ShowMessage("Podaj kod karty, którą przybliżyłeś: ");
            cardCode = con.ReadString();

            machine.AnnounceSwipe(cardCode, device);
        }

        private static void IncorrectCommand()
        {
            con.ShowMessage("Nieprawidłowe polecenie!");
            con.ShowMessage("");
            con.ShowMessage("");
        }
        
    }
}
