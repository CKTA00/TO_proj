using ParkingApplication.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingApplication.DeviceInterface
{
    interface IStandardButtonsAPI
    {
        void AddButtonObserver(ButtonKey key, IButtonObserver observer);
        void RemoveButtonObserver(ButtonKey key, IButtonObserver observer);
        void AnnounceButtonPressedAll(ButtonKey key);
        void AnnounceButtonPressed(ButtonKey key, IButtonObserver observer);
    }
}
