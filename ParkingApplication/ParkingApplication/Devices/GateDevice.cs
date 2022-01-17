using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingApplication.ParkingSystem;
using ParkingApplication.DeviceInterface;

namespace ParkingApplication.Devices
{
    abstract class GateDevice : Device, IPremiumCardObserver
    {
        protected TicketDatabase normalTicketsDB;
        protected TicketDatabase handicappedTicketsDB;
        protected IGateAPI gate;
        public GateDevice(ISimpleDialog initDisplay, IGateAPI gate, TicketDatabase normalTicketsDB, TicketDatabase handicappedTicketsDB) : base(initDisplay)
        {
            this.normalTicketsDB = normalTicketsDB;
            this.handicappedTicketsDB = handicappedTicketsDB;
            this.gate = gate;
        }

        public abstract void CardSwiped(string code);
    }
}
