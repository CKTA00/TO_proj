using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ParkingApplication.ParkingSystem;
using ParkingApplication.Devices;
using ParkingApplication.DeviceInterface;
using ParkingApplication.Premium;
using ParkingApplication.CashSystem;

namespace ParkingApplication
{
    class DeviceBuilder
    {
        private static DeviceBuilder instance;

        // devices parts:
        ISimpleDialog dialog;
        IGateAPI gate;
        IPrinterAPI ticketPrinter;
        IScannerAPI scanner;
        IPremiumCardAPI cardReaader;
        IStandardButtonsAPI buttons;
        ICashMachineOutput cashOutput;

        // database:
        TicketDatabase normalTicketDB;
        TicketDatabase handicappedTicketDB;
        PremiumDatabase premiumDatabase;

        internal ISimpleDialog Dialog { get => dialog; set => dialog = value; }
        internal IGateAPI Gate { get => gate; set => gate = value; }
        internal IPrinterAPI TicketPrinter { get => ticketPrinter; set => ticketPrinter = value; }
        internal IScannerAPI Scanner { get => scanner; set => scanner = value; }
        internal IPremiumCardAPI CardReaader { get => cardReaader; set => cardReaader = value; }
        internal IStandardButtonsAPI Buttons { get => buttons; set => buttons = value; }
        internal ICashMachineOutput CashOutput { get => cashOutput; set => cashOutput = value; }

        internal TicketDatabase NormalTicketDB { get => normalTicketDB; set => normalTicketDB = value; }
        internal TicketDatabase HandicappedTicketDB { get => handicappedTicketDB; set => handicappedTicketDB = value; }
        internal PremiumDatabase PremiumDatabase { get => premiumDatabase; set => premiumDatabase = value; }

        private DeviceBuilder()
        {
            
        }

        public static DeviceBuilder GetInstance()
        {
            if (instance == null)
                instance = new DeviceBuilder();
            return instance;
        }

        internal EntranceParkingDevice BuildEntranceParkingDevice()
        {
            EntranceParkingDevice ret = new EntranceParkingDevice(dialog, gate, ticketPrinter, normalTicketDB, handicappedTicketDB, premiumDatabase);
            buttons.AddButtonObserver(ButtonKey.ACCEPT_BUTTON, ret);
            buttons.AddButtonObserver(ButtonKey.SPECIAL_BUTTON, ret);
            cardReaader.AddPremiumCardObserver(ret);
            return ret;
        }

        internal RegisterDevice BuildRegisterDevice()
        {
            CoinContainer bank = new CoinContainer(cashOutput);
            RegisterDevice ret = new RegisterDevice(dialog, premiumDatabase, bank, new TicketPrices(), new PremiumPrices());
            bank.SetContext(ret, dialog);
            scanner.AddScannerObserver(ret);
            cardReaader.AddPremiumCardObserver(ret);
            buttons.AddButtonObserver(ButtonKey.ACCEPT_BUTTON, ret);
            buttons.AddButtonObserver(ButtonKey.SPECIAL_BUTTON, ret);
            buttons.AddButtonObserver(ButtonKey.CANCEL_BUTTON, ret);
            return ret;
        }

        internal ExitParkingDevice BuildExitParkingDevice()
        {
            ExitParkingDevice ret = new ExitParkingDevice(dialog, gate, normalTicketDB, handicappedTicketDB, premiumDatabase);
            scanner.AddScannerObserver(ret);
            return ret;
        }
    }
}
