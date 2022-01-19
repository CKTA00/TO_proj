using System;

namespace ParkingApplication.ParkingSystem
{
    class InvalidTicketCodeException : Exception
    {
        public InvalidTicketCodeException() : base("Invalid code!")
        {

        }
    }
}
