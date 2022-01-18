using System;
using System.Collections.Generic;
using System.Linq;
using ParkingApplication.DeviceInterface;
using ParkingApplication.Premium;

namespace ParkingApplication.Devices
{
    abstract class Device : IButtonObserver, IPremiumCardObserver
    {
        protected ISimpleDialog display;
        protected Dictionary<ButtonKey, List<IButtonObserver>> observers;
        protected PremiumDatabase premiumDB;

        public Device(ISimpleDialog initDisplay, PremiumDatabase premiumDB)
        {
            display = initDisplay;
            this.premiumDB = premiumDB;
        }

        public abstract void ButtonPressed(ButtonKey key);
        public abstract void CardSwiped(string code);
        public abstract void Main();
    }
}
