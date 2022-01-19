using ParkingApplication.DeviceInterface;
using ParkingApplication.ParkingSystem;
using ParkingApplication.Premium;

namespace ParkingApplication.Devices
{
    abstract class Device : IButtonObserver, IPremiumCardObserver
    {
        protected ISimpleDialog display;
        protected TicketDatabase normalTicketsDB;
        protected TicketDatabase handicappedTicketsDB;
        protected PremiumDatabase premiumDB;

        public Device(ISimpleDialog initDisplay, TicketDatabase normalDB, TicketDatabase handicappedDB, PremiumDatabase premiumDB)
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
