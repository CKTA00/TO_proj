using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ParkingApplication.ParkingSystem;
using ParkingApplication.Devices;
using ParkingApplication.DeviceInterface;
using ParkingApplication.Premium;

namespace ParkingApplication
{
    class DeviceFactory
    {
        private static DeviceFactory instance;

        // devices parts:
        ISimpleDialog dialog;
        IGateAPI gate;
        IPrinterAPI ticketPrinter;
        IScannerAPI scanner;
        IPremiumCardAPI cardReaader;
        IStandardButtonsAPI buttons;

        // database:
        TicketDatabase normalTicketDB;
        TicketDatabase handicappedTicketDB;
        PremiumDatabase premiumDatabase;

        // device containers: // TODO: Move somewhere else
        List<EntranceParkingDevice> entanceDevices;
        List<ExitParkingDevice> exitDevices;
        //TODO: lists of devices of each type

        internal ISimpleDialog Ui { get => dialog; set => dialog = value; }
        internal IGateAPI Gate { get => gate; set => gate = value; }
        internal IPrinterAPI TicketPrinter { get => ticketPrinter; set => ticketPrinter = value; }
        internal IScannerAPI Scanner { get => scanner; set => scanner = value; }
        internal IPremiumCardAPI CardReaader { get => cardReaader; set => cardReaader = value; }
        internal IStandardButtonsAPI Buttons { get => buttons; set => buttons = value; }
        internal TicketDatabase NormalTicketDB { get => normalTicketDB; set => normalTicketDB = value; }
        internal TicketDatabase HandicappedTicketDB { get => handicappedTicketDB; set => handicappedTicketDB = value; }
        internal PremiumDatabase PremiumDatabase { get => premiumDatabase; set => premiumDatabase = value; }

        private DeviceFactory()
        {
            
        }

        public static DeviceFactory GetInstance()
        {
            if (instance == null)
                instance = new DeviceFactory();
            return instance;
        }

        public void Run()
        {
            entanceDevices = new List<EntranceParkingDevice>();
            entanceDevices.Add(new EntranceParkingDevice(dialog,gate,ticketPrinter,normalTicketDB,handicappedTicketDB,premiumDatabase));
            foreach(EntranceParkingDevice o in entanceDevices)
            {
                buttons.AddButtonObserver(ButtonKey.ACCEPT_BUTTON, o);
                buttons.AddButtonObserver(ButtonKey.SPECIAL_BUTTON, o);
                cardReaader.AddPremiumCardObserver(o);
            }

            exitDevices = new List<ExitParkingDevice>();
            exitDevices.Add(new ExitParkingDevice(dialog, gate, normalTicketDB, handicappedTicketDB, premiumDatabase));
            foreach (ExitParkingDevice o in exitDevices)
            {
                //buttons.AddButtonObserver(ButtonKey.ACCEPT_BUTTON, o);
                //buttons.AddButtonObserver(ButtonKey.SPECIAL_BUTTON, o);
                scanner.AddScannerObserver(o);
            }

            
            // TODO: maybe run each device on its own thread?
            //entanceDevices[0].Main();
        }

        public List<EntranceParkingDevice> GetEntranceDevices()
        {
            return entanceDevices;
        }
    }
}
