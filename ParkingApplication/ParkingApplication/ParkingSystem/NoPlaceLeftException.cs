using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingApplication.ParkingSystem
{
    class NoPlaceLeftException : Exception
    {
        public NoPlaceLeftException():base("Not enough places for new car!")
        {

        }
    }
}
