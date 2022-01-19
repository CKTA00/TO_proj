using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingApplication.CashSystem;
using ParkingApplication.Devices;
using ParkingApplication.ParkingSystem;

namespace ParkingApplication.DeviceInterface
{
    class ConsoleMachineAPI : ISimpleDialog, IGateAPI, IPrinterAPI, ICashMachineAPI, IStandardButtonsAPI, IScannerAPI, IPremiumCardAPI, ICashMachineOutput, IPremiumPrinter
    {
        ConsoleDisplay con;
        Dictionary<ButtonKey, List<IButtonObserver>> buttonObservers;
        List<IPremiumCardObserver> cardObservers;
        List<ICodeScannerObserver> scanerObservers;
        List<ICashMachineObserver> cashObservers;

        public ConsoleMachineAPI()
        {
            // current implementation uses console. Might use WPF desktop window.
            con = ConsoleDisplay.GetInstance();

            //init observer containers
            buttonObservers = new Dictionary<ButtonKey, List<IButtonObserver>>();
            // TODO: think of a better solution
            buttonObservers.Add(ButtonKey.ACCEPT_BUTTON, new List<IButtonObserver>());
            buttonObservers.Add(ButtonKey.CANCEL_BUTTON, new List<IButtonObserver>());
            buttonObservers.Add(ButtonKey.SPECIAL_BUTTON, new List<IButtonObserver>());
            cardObservers = new List<IPremiumCardObserver>();
            scanerObservers = new List<ICodeScannerObserver>();
            cashObservers = new List<ICashMachineObserver>();
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
            con.ShowMessage("[Wysuwa bilet z kodem kreskowym " + ticket.Code + "]");
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

        public void AddButtonObserver(ButtonKey key, IButtonObserver observer)
        {
            List<IButtonObserver> list = buttonObservers[key]; //maybe add button dictionary key here, if doesn't exist
            if (!list.Contains(observer))
            {
                list.Add(observer);
            }
        }

        public void RemoveButtonObserver(ButtonKey key, IButtonObserver observer)
        {
            List<IButtonObserver> list = buttonObservers[key];
            if (!list.Contains(observer))
            {
                list.Remove(observer);
            }
        }

        public void AnnounceButtonPressedAll(ButtonKey key)
        {
            List<IButtonObserver> list = buttonObservers[key];
            foreach (IButtonObserver observer in list)
            {
                observer.ButtonPressed(key);
            }
        }

        public void AnnounceButtonPressed(ButtonKey key, IButtonObserver observer)
        {
            List<IButtonObserver> list = buttonObservers[key];
            if (list.Contains(observer)) observer.ButtonPressed(key);
        }

        public void InsertCoin(AllowedDenominations denomination)
        {
            throw new NotImplementedException();
        }

        public void PayByDebitCard(string debitCardData, int valuePLN, int valueGR)
        {
            throw new NotImplementedException();
        }

        public void AddScannerObserver(ICodeScannerObserver observer)
        {
            scanerObservers.Add(observer);
        }

        public void RemoveScannerObserver(ICodeScannerObserver observer)
        {
            scanerObservers.Remove(observer);
        }

        public void AnnounceScanAll(string code)
        {
            foreach(ICodeScannerObserver o in scanerObservers)
            {
                o.CodeScanned(code);
            }
        }

        public void AnnounceScan(string code, ICodeScannerObserver observer)
        {
            if (scanerObservers.Contains(observer)) observer.CodeScanned(code);
        }

        public void AddPremiumCardObserver(IPremiumCardObserver observer)
        {
            cardObservers.Add(observer);
        }

        public void RemovePremiumCardObserver(IPremiumCardObserver observer)
        {
            cardObservers.Remove(observer);
        }

        public void AnnounceSwipeAll(string data)
        {
            foreach (IPremiumCardObserver o in cardObservers) o.CardSwiped(data);
        }

        public void AnnounceSwipe(string data, IPremiumCardObserver observer)
        {
            if (cardObservers.Contains(observer)) observer.CardSwiped(data);
        }

        public void AddCashMachineObserver(ICashMachineObserver o)
        {
            cashObservers.Add(o);
        }

        public void RemoveCashMachineObserver(ICashMachineObserver o)
        {
            cashObservers.Remove(o);
        }

        public void InsertCoin(AllowedDenominations den, ICashMachineObserver o)
        {
            if (cashObservers.Contains(o)) o.InsertCoin(den);
        }

        public void ThrowCoin(AllowedDenominations den)
        {
            string coinName;
            int v = (int)den;
            if(v >= 100)
            {
                coinName = v / 100 + " zł";
            }
            else
            {
                coinName = v + " gr";
            }
            con.ShowMessage("Z automatu wypadła moneta "+coinName);
        }

        public void ThrowCoins(AllowedDenominations den, int amount)
        {
            string coinName;
            int v = (int)den;
            if (v >= 100)
            {
                coinName = v / 100 + " zł";
            }
            else
            {
                coinName = v + " gr";
            }
            con.ShowMessage("Z automatu wypadło "+amount+" monet " + coinName);
        }

        public void PrintPremiumCard(string code)
        {
            con.ShowMessage("[Wysuwa plastikową karte z maskiem magnetycznym i numerem " + code + "]");
        }
    }
}
