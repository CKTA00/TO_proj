using ParkingApplication.ParkingSystem;
using ParkingApplication.DeviceInterface;
using ParkingApplication.Premium;

namespace ParkingApplication.Devices
{
    abstract class GateDevice : Device
    {
        protected IGateAPI gate;
        public GateDevice(ISimpleDialog initDisplay, IGateAPI gate,
            TicketDatabase normalTicketsDB, TicketDatabase handicappedTicketsDB, PremiumDatabase premiumDB)
            : base(initDisplay, normalTicketsDB, handicappedTicketsDB, premiumDB)
        {
            this.gate = gate;
        }
    }
}
