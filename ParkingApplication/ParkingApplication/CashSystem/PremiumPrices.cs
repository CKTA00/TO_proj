using System;

namespace ParkingApplication.CashSystem
{
    class PremiumPrices : IPriceStrategy
    {
        public int CalculatePriceInGr(TimeSpan t)
        {
            t = t.Duration();
            int days = (int)t.TotalDays;
            return days*5/3;
        }
    }
}
