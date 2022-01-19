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
        IPremiumCardAPI cardReader;
        IStandardButtonsAPI buttons;
        ICashMachineOutput cashOutput;
        ICashMachineAPI cashMachine;

        // database:
        ITicketDatabase normalTicketDB;
        ITicketDatabase handicappedTicketDB;
        IPremiumDatabase premiumDatabase;

        internal ISimpleDialog Dialog { get => dialog; set => dialog = value; }
        internal IGateAPI Gate { get => gate; set => gate = value; }
        internal IPrinterAPI TicketPrinter { get => ticketPrinter; set => ticketPrinter = value; }
        internal IScannerAPI Scanner { get => scanner; set => scanner = value; }
        internal IPremiumCardAPI CardReader { get => cardReader; set => cardReader = value; }
        internal IStandardButtonsAPI Buttons { get => buttons; set => buttons = value; }
        internal ICashMachineOutput CashOutput { get => cashOutput; set => cashOutput = value; }
        internal ICashMachineAPI CashMachine { get => cashMachine; set => cashMachine = value; }

        internal ITicketDatabase NormalTicketDB { get => normalTicketDB; set => normalTicketDB = value; }
        internal ITicketDatabase HandicappedTicketDB { get => handicappedTicketDB; set => handicappedTicketDB = value; }
        internal IPremiumDatabase PremiumDatabase { get => premiumDatabase; set => premiumDatabase = value; }

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
            cardReader.AddPremiumCardObserver(ret);
            return ret;
        }

        internal RegisterDevice BuildRegisterDevice()
        {
            CoinContainer bank = new CoinContainer(cashOutput);
            RegisterDevice ret = new RegisterDevice(dialog, normalTicketDB, handicappedTicketDB, premiumDatabase, bank, new TicketPrices(), new PremiumPrices());
            bank.SetContext(ret, dialog);
            cashMachine.AddCashMachineObserver(bank);
            scanner.AddScannerObserver(ret);
            cardReader.AddPremiumCardObserver(ret);
            buttons.AddButtonObserver(ButtonKey.ACCEPT_BUTTON, ret);
            buttons.AddButtonObserver(ButtonKey.SPECIAL_BUTTON, ret);
            buttons.AddButtonObserver(ButtonKey.CANCEL_BUTTON, ret);
            return ret;
        }

        internal ExitParkingDevice BuildExitParkingDevice()
        {
            ExitParkingDevice ret = new ExitParkingDevice(dialog, gate, normalTicketDB, handicappedTicketDB, premiumDatabase);
            scanner.AddScannerObserver(ret);
            cardReader.AddPremiumCardObserver(ret);
            return ret;
        }
    }
}
