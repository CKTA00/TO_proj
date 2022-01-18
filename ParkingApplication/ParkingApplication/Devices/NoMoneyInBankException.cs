using System;
using System.Runtime.Serialization;

namespace ParkingApplication.Devices
{
    [Serializable]
    internal class NoMoneyInBankException : Exception
    {
        public NoMoneyInBankException() : base("Not enough coins in coin bank. Notify the owner.")
        {

        }
    }
}