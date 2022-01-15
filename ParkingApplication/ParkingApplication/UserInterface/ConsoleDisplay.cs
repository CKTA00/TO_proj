using System;

namespace ParkingApplication.UserInterface
{
    class ConsoleDisplay : ISimpleDialog
    {
        static ConsoleDisplay instance;

        private ConsoleDisplay(){}

        public static ConsoleDisplay GetInstance()
        {
            if (instance==null)
                instance = new ConsoleDisplay();
            return instance;
        }

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
