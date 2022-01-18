using System;

namespace ParkingApplication.CashSystem
{
    internal interface IPriceStrategy
    {
        int CalculatePriceInGr(TimeSpan t);
    }
}