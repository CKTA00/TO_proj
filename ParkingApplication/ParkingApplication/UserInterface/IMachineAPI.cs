using ParkingApplication.ParkingSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingApplication.UserInterface
{
    interface IMachineAPI
    {
        void PrintTicket(Ticket t);
        void OpenGate();
    }
}
