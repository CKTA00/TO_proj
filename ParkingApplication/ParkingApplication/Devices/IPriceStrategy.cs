using System;

namespace ParkingApplication.Devices
{
    internal interface IPriceStrategy
    {
        void CalculatePrice(TimeSpan t);
    }
}