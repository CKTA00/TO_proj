using System;

namespace ParkingApplication.ParkingSystem
{
    class Ticket
    {
        private string code;
        private DateTime entranceTime;
        public string Code { get => code; }
        public DateTime EntranceTime { get => entranceTime; }

        public Ticket(string code, DateTime entranceTime){
            this.code = code;
            this.entranceTime = entranceTime;
        }
    }
}
