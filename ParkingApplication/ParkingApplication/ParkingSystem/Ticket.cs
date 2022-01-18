using System;

namespace ParkingApplication.ParkingSystem
{
    public class Ticket
    {
        private string code;
        private DateTime entranceTime;
        private bool isPaid;
        public string Code { get => code; }
        public DateTime EntranceTime { get => entranceTime; }
        public bool IsPaid { get => isPaid; }

        public Ticket(string code, DateTime entranceTime){
            this.code = code;
            this.entranceTime = entranceTime;
            isPaid = false;
        }

        public void Realize()
        {
            isPaid = true;
        }
    }
}
