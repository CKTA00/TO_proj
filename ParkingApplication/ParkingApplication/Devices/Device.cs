using System;
using System.Collections.Generic;
using System.Linq;
using ParkingApplication.DeviceInterface;

namespace ParkingApplication.Devices
{
    abstract class Device : IButtonObserver
    {
        protected ISimpleDialog display;
        protected Dictionary<ButtonKey, List<IButtonObserver>> observers;

        public Device(ISimpleDialog initDisplay)
        {
            display = initDisplay;
        }

        public abstract void ButtonPressed(ButtonKey key);
        public abstract void Main();
    }
}
