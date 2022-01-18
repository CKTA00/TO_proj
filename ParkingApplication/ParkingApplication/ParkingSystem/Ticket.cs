using System;

namespace ParkingApplication.ParkingSystem
{
    public class Ticket
    {
        private string code;
        private DateTime entranceTime;
        private bool isPaid;
        private DateTime paymentTime;

        public string Code { get => code; }
        public DateTime EntranceTime { get => entranceTime; }
        public DateTime PaymentTime { get => paymentTime; }
        public bool IsPaid { get => isPaid; }

        public Ticket(string code, DateTime entranceTime){
            this.code = code;
            this.entranceTime = entranceTime;
            isPaid = false;
        }

        public void Realize()
        {
            isPaid = true;
            paymentTime = DateTime.Now;
        }
        
        public void Underpaid()
        {
            isPaid = false;
            entranceTime = paymentTime;
        }
    }
}
