using System;
using ParkingApplication.UserInterface;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingApplication.ParkingSystem;

namespace ParkingApplication
{
    class Application
    {
        static TestingConsoleUI ui;
        static TicketDatabase ticketDB;
        static void Main(string[] args)
        {
            InitializeSystem();
            ui.ShowSimulationMenu();
        }

        static void InitializeSystem()
        {
            ui = new TestingConsoleUI();
            ticketDB = new TicketDatabase();
        }
    }
}
