using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingApplication.ParkingSystem
{
    class InvalidTicketCodeException : Exception
    {
        public InvalidTicketCodeException() : base("Invalid code!")
        {

        }
    }
}
