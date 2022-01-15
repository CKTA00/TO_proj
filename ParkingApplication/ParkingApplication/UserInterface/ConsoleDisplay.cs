using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingApplication.UserInterface
{
    class ConsoleDisplay : IDisplay
    {
        public string ReadString()
        {
            System.Console.Write(">");
            return System.Console.ReadLine();
        }

        public void ShowMessage(string msg)
        {
            System.Console.WriteLine(msg);
        }
    }
}
