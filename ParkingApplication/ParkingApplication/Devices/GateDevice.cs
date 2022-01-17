using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingApplication.ParkingSystem;
using ParkingApplication.UserInterface;

namespace ParkingApplication.Devices
{
    abstract class GateDevice : Device
    {
        protected TicketDatabase normalTicketsDB;
        protected TicketDatabase handicappedTicketsDB;
        protected IMachineAPI machine;
        public GateDevice(ISimpleDialog initDisplay, IMachineAPI machine, TicketDatabase normalTicketsDB, TicketDatabase handicappedTicketsDB) : base(initDisplay)
        {
            this.normalTicketsDB = normalTicketsDB;
            this.handicappedTicketsDB = handicappedTicketsDB;
            this.machine = machine;
        }
    }
}
