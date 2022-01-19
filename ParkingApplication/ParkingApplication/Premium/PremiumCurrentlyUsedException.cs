using System;

namespace ParkingApplication.Premium
{
    class PremiumCurrentlyUsedException : Exception
    {
        public PremiumCurrentlyUsedException() : base("Your card is already used")
        {

        }
    }
}
