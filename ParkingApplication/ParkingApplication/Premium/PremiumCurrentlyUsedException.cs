using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingApplication.Premium
{
    class PremiumCurrentlyUsedException : Exception
    {
        public PremiumCurrentlyUsedException() : base("Your card is already used")
        {

        }
    }
}
