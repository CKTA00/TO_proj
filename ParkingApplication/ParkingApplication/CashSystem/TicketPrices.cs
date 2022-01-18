using System;

namespace ParkingApplication.CashSystem
{
    class TicketPrices : IPriceStrategy
    {
        public int CalculatePriceInGr(TimeSpan t)
        {
            t = t.Duration();
            int minutes = (int)t.TotalMinutes;
            if(minutes<10)
            {
                return 20;
            }
            else
            {
                return (2 * minutes)-(2*minutes)%10;
            }
        }
    }
}
