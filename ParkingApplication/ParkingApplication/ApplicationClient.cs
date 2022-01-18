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
using ParkingApplication.CashSystem;

namespace ParkingApplication
{
    class ApplicationClient
    {
        static ConsoleMachineAPI machine;
        static ICodeGenerator generator;
        static ISimpleDialog con;
        static DeviceBuilder builder;

        static List<EntranceParkingDevice> entanceDevices;
        static List<ExitParkingDevice> exitDevices;
        static List<RegisterDevice> registerDevices;

        static void Main(string[] args)
        {
            con = ConsoleDisplay.GetInstance();
            machine = new ConsoleMachineAPI();
            generator = new GUIDGenerator();

            TicketDatabase normalTicketDB = new TicketDatabase(generator, 40);
            TicketDatabase handicappedTicketDB = new TicketDatabase(generator, 5);
            PremiumDatabase premiumDatabase = new PremiumDatabase(generator);

            builder = DeviceBuilder.GetInstance();
            builder.Buttons = machine;
            builder.CardReaader = machine;
            builder.Gate = machine;
            builder.HandicappedTicketDB = handicappedTicketDB;
            builder.NormalTicketDB = normalTicketDB;
            builder.PremiumDatabase = premiumDatabase;
            builder.Scanner = machine;
            builder.TicketPrinter = machine;
            builder.Dialog = machine;

            entanceDevices = new List<EntranceParkingDevice>();
            entanceDevices.Add(builder.BuildEntranceParkingDevice());

            exitDevices = new List<ExitParkingDevice>();
            exitDevices.Add(builder.BuildExitParkingDevice());

            registerDevices = new List<RegisterDevice>();
            registerDevices.Add(builder.BuildRegisterDevice());

            ShowSimulationMenu();
        }

        static void ShowSimulationMenu()
        {
            while (true)
            {
                con.ShowMessage("\n\nZasymuluj działanie parkomatu. Wpisz jeden z poniższych usecaseów:");
                con.ShowMessage(">drive in");
                con.ShowMessage("\tSymuluje wjazd nowego użytkownika na parking.");
                con.ShowMessage(">drive out");
                con.ShowMessage("\tSymuluje wyjazd losowego użytkownika z parkingu.");
                con.ShowMessage(">payment");
                con.ShowMessage("\tSymuluje zapłate za bilet lub rejestracje i przedłużenie karty premium.");
                con.ShowMessage(">exit");
                con.ShowMessage("\tWyjdź.");
                string command = con.ReadString();
                if (command == "drive in") //might use chain of responsibility if enough time
                {
                    DriveIn();
                }
                else if (command == "drive out")
                {
                    DriveOut();
                }
                else if (command == "payment")
                {
                    Payment();
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

        static void DriveIn()
        {
            EntranceParkingDevice device = entanceDevices[0];
            device.Main();

            while (true)
            {
                con.ShowMessage("\n\n>press accept");
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

        static void DriveOut()
        {
            ExitParkingDevice device = exitDevices[0];
            device.Main();

            while (true)
            {
                con.ShowMessage("\n\n>scan");
                con.ShowMessage("\tZeskanuj bilet.");
                con.ShowMessage(">card");
                con.ShowMessage("\tPrzyłóż kartę.");
                con.ShowMessage(">exit");
                con.ShowMessage("\tZawróć.");
                string command = con.ReadString();
                if (command == "scan")
                {
                    Scan(device);
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

        private static void Payment()
        {
            RegisterDevice device = registerDevices[0];
            device.Main();

            while (true)
            {
                con.ShowMessage("\n\n>press accept");
                con.ShowMessage("\tNaciśnij główny przycisk.");
                con.ShowMessage(">press cancel");
                con.ShowMessage("\tNaciśnij anuluj.");
                con.ShowMessage(">card");
                con.ShowMessage("\tPrzyłóż kartę.");
                con.ShowMessage(">scan");
                con.ShowMessage("\tZeskanuj bilet.");
                con.ShowMessage(">scan");
                con.ShowMessage("\tWpłać pieniądze.");
                con.ShowMessage(">exit");
                con.ShowMessage("\tZawróć.");
                string command = con.ReadString();
                if (command == "press accept")
                {
                    machine.AnnounceButtonPressed(ButtonKey.ACCEPT_BUTTON, device);
                }
                else if (command == "press cancel")
                {
                    machine.AnnounceButtonPressed(ButtonKey.CANCEL_BUTTON, device);
                }
                else if (command == "card")
                {
                    SwipeCard(device);
                }
                else if (command == "scan")
                {
                    Scan(device);
                }
                else if(command == "insert")
                {
                    Insert(device.Bank);
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

        private static void Insert(CoinContainer device)
        {
            while (true)
            {
                con.ShowMessage("\nPodaj wartość monety w groszach (od 10 gr w góre) lub exit:");
                string scoin = con.ReadString();
                AllowedDenominations coin;
                switch (scoin)
                {
                    case "10":
                        coin = AllowedDenominations.M10gr;
                        break;
                    case "20":
                        coin = AllowedDenominations.M20gr;
                        break;
                    case "50":
                        coin = AllowedDenominations.M50gr;
                        break;
                    case "100":
                        coin = AllowedDenominations.M1pln;
                        break;
                    case "200":
                        coin = AllowedDenominations.M2pln;
                        break;
                    case "500":
                        coin = AllowedDenominations.M5pln;
                        break;
                    case "exit":
                        return;
                    default:
                        IncorrectCommand();
                        continue;
                }
                machine.InsertCoin(coin,device);
            }
            
        }

        private static void SwipeCard(IPremiumCardObserver device)
        {
            string cardCode;

            con.ShowMessage("Podaj kod karty, którą przybliżyłeś: ");
            cardCode = con.ReadString();

            machine.AnnounceSwipe(cardCode, device);
        }

        private static void Scan(ICodeScannerObserver device)
        {
            string code;

            con.ShowMessage("Podaj kod zeskanowanego biletu: ");
            code = con.ReadString();

            machine.AnnounceScan(code, device);
        }

        private static void IncorrectCommand()
        {
            con.ShowMessage("Nieprawidłowe polecenie!");
            con.ShowMessage("");
            con.ShowMessage("");
        }
        
    }
}
