using System;
using System.Runtime.Serialization;

namespace ParkingApplication.Premium
{
    [Serializable]
    internal class InvalidPremiumUserCodeException : Exception
    {
        public InvalidPremiumUserCodeException() : base("Cannot find user in database")
        {
        }
    }
}