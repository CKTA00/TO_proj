using System;
using System.Collections.Generic;
using System.Linq;
using ParkingApplication.UserInterface;

namespace ParkingApplication.Devices
{
    abstract class Device
    {
        protected ISimpleDialog display;
        protected Dictionary<ButtonKey, List<IGuiEventListener>> observers;

        public Device(ISimpleDialog initDisplay)
        {
            display = initDisplay;
            observers = new Dictionary<ButtonKey, List<IGuiEventListener>>();
            // TODO: think of a better solution
            observers.Add(ButtonKey.ACCEPT_BUTTON, new List<IGuiEventListener>());
        }

        public void AddButtonObserver(ButtonKey key, IGuiEventListener observer)
        {
            List<IGuiEventListener> list = observers[key]; //maybe add button dictionary key here, if doesn't exist
            if (!list.Contains(observer))
            {
                list.Add(observer);
            }
        }

        public void RemoveButtonObserver(ButtonKey key, IGuiEventListener observer)
        {
            List<IGuiEventListener> list = observers[key];
            if (!list.Contains(observer))
            {
                list.Remove(observer);
            }
        }

        protected void NotifyAllButtonPressed(ButtonKey key)
        {
            List<IGuiEventListener> list = observers[key];
            foreach (IGuiEventListener observer in list)
            {
                observer.ButtonPressed();
            }
        }

        abstract public void AcceptButtonPressed();

        abstract public void Main();
    }
}
