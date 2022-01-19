using ParkingApplication.DeviceInterface;
using ParkingApplication.ParkingSystem;
using ParkingApplication.Premium;

namespace ParkingApplication.Devices
{
    abstract class Device : IButtonObserver, IPremiumCardObserver
    {
        protected ISimpleDialog display;
        protected ITicketDatabase normalTicketsDB;
        protected ITicketDatabase handicappedTicketsDB;
        protected IPremiumDatabase premiumDB;

        public Device(ISimpleDialog initDisplay, ITicketDatabase normalDB, ITicketDatabase handicappedDB, IPremiumDatabase premiumDB)
        {
            display = initDisplay;
            this.normalTicketsDB = normalDB;
            this.handicappedTicketsDB = handicappedDB;
            this.premiumDB = premiumDB;

        }

        public abstract void ButtonPressed(ButtonKey key);
        public abstract void CardSwiped(string code);
        public abstract void Main();
    }
}
