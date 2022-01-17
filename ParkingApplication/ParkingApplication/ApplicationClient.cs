using System;
using ParkingApplication.Devices;
using ParkingApplication.UserInterface;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingApplication
{
    class ApplicationClient
    {
        static ISimpleDialog con;
        static Application app;
        static void Main(string[] args)
        {
            con = ConsoleDisplay.GetInstance();
            app = Application.GetInstance();
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
            EntranceParkingMachine device = app.GetEntranceDevices()[0];
            device.Main();

            while (true)
            {
                con.ShowMessage(">press");
                con.ShowMessage("\tNaciśnij.");
                con.ShowMessage(">card");
                con.ShowMessage("\tPrzyłóż kartę.");
                con.ShowMessage(">exit");
                con.ShowMessage("\tZawróć.");
                string command = con.ReadString();
                if (command == "press")
                {
                    device.AcceptButtonPressed();
                    con.ShowMessage("Zaparkowałeś!");
                    con.ShowMessage("");
                    con.ShowMessage("");
                    break;
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

        private static void SwipeCard(EntranceParkingMachine device)
        {
            int cardId;
            string inputString;
            while (true)
            {
                con.ShowMessage("Podaj ID karty, którą przybliżyłeś: ");
                inputString = con.ReadString();
                if (int.TryParse(inputString,out cardId))
                {
                    break;
                }
                else
                {
                    con.ShowMessage("Nieprawidłowe id");
                    con.ShowMessage("");
                    con.ShowMessage("");
                }
            }
            
            device.CheckCard(cardId);
        }

        private static void IncorrectCommand()
        {
            con.ShowMessage("Nieprawidłowe polecenie!");
            con.ShowMessage("");
            con.ShowMessage("");
        }
        
    }
}
