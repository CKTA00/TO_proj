using System;
using System.Collections.Generic;
using System.Linq;


namespace ParkingApplication.UserInterface
{
    interface IPaymentDevice
    {
        void DisposeChange();
        void AddButtonObserver(ButtonKey key, IGuiEventListener observer);
        void RemoveButtonObserver(ButtonKey key, IGuiEventListener observer);
    }
}
