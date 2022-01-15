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
        MachineScreenDisplay ui;
        TicketDatabase ticketDB;
        List<EntranceParkingMachine> entanceDevices;
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
            ui = new MachineScreenDisplay();
            ticketDB = new TicketDatabase();
            entanceDevices = new List<EntranceParkingMachine>();
            entanceDevices.Add(new EntranceParkingMachine(ui));
            //entanceDevices[0].Main();
        }

        public List<EntranceParkingMachine> GetEntranceDevices()
        {
            return entanceDevices;
        }
    }
}
