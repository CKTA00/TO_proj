using System;

namespace ParkingApplication.ParkingSystem
{
    class NoPlaceLeftException : Exception
    {
        public NoPlaceLeftException():base("Not enough places for new car!")
        {

        }
    }
}
