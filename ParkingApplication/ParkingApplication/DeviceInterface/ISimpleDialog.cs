using System;

namespace ParkingApplication.DeviceInterface
{
    interface ISimpleDialog
    {
        void ShowMessage(String msg);
        String ReadString();
    }
}
