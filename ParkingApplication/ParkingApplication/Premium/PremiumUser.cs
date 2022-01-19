using ParkingApplication.ParkingSystem;
using System;

namespace ParkingApplication.Premium
{
    class PremiumUser
    {
        private string code;
        private DateTime expiryDate;
        private string registrationPlate;
        private string currentTicketCode;
        private bool isHandicapped;
        public string Code { get => code; }
        public DateTime ExpiryDate { get => expiryDate; }
        public string RegistrationPlate { get => registrationPlate; set => registrationPlate = value; }
        public bool IsHandicapped { get => isHandicapped; set => isHandicapped = value; }

        public PremiumUser(string code, DateTime expiryTime, string registrationPlate)
        {
            this.code = code;
            this.expiryDate = expiryTime;
            this.registrationPlate = registrationPlate;
            currentTicketCode = "";
        }

        public void AddTicket(Ticket ticket)
        {
            if (currentTicketCode.Equals(""))
                currentTicketCode = ticket.Code;
            else
                throw new PremiumCurrentlyUsedException();
        }

        public void RemoveTicket()
        {
            currentTicketCode = "";
        }

        public void Extend(TimeSpan time)
        {
            expiryDate += time;
        }
    }
}
