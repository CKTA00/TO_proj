using System;

namespace ParkingApplication.Premium
{
    class PremiumUser
    {
        private string code;
        private DateTime expityDate;
        private string registrationPlate;
        public string Code { get => code; }
        public DateTime EntranceTime { get => expityDate; }
        public string RegistrationPlate { get => registrationPlate; set => registrationPlate = value; }

        public PremiumUser(string code, DateTime entranceTime){
            this.code = code;
            this.expityDate = entranceTime;
            registrationPlate = "";
        }
    }
}
