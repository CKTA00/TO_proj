using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ParkingApplication.ParkingSystem;
using ParkingApplication.Devices;
using ParkingApplication.UserInterface;

namespace ParkingApplication
{
    class Application
    {
        ISimpleDialog ui;
        TicketDatabase normalTicketDB;
        TicketDatabase handicappedTicketDB;
        List<EntranceParkingMachine> entanceDevices;
        IMachineAPI entranceMachine;
        //TODO: lists of devices of each type

        private static Application instance;

        private Application()
        {
            Init();
        }

        public static Application GetInstance()
        {
            if (instance == null)
                instance = new Application();
            return instance;
        }

        private void Init()
        {
            ui = ConsoleDisplay.GetInstance();
            ICodeGenerator generator = new GUIDGenerator();
            normalTicketDB = new TicketDatabase(generator,40);
            handicappedTicketDB = new TicketDatabase(generator,5);

            entranceMachine = new ConsoleMachineAPI();
            entanceDevices = new List<EntranceParkingMachine>();
            entanceDevices.Add(new EntranceParkingMachine(ui,entranceMachine,normalTicketDB,handicappedTicketDB));
            // TODO: maybe run each device on its own thread?
            //entanceDevices[0].Main();
        }

        public List<EntranceParkingMachine> GetEntranceDevices()
        {
            return entanceDevices;
        }
    }
}
