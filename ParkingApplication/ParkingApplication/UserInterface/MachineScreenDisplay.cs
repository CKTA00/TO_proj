using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingApplication.UserInterface
{
    class MachineScreenDisplay : ISimpleDialog, IDisplay
    {
        ConsoleDisplay con;

        public MachineScreenDisplay()
        {
            // current implementation uses console. Might use WPF desktop window.
            con = ConsoleDisplay.GetInstance();
        }

        public void OpenGate()
        {
            throw new NotImplementedException();
        }

        public string ReadString()
        {
            con.ShowMessage("[Klawiatura Automatu]");
            return con.ReadString();
        }

        public void ShowMessage(string msg)
        {
            con.ShowMessage("[Automat] " + msg);
        }
    }
}
