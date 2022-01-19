namespace ParkingApplication.ParkingSystem
{
    interface ITicketDatabase
    {
        Ticket TryAddTicket();

        Ticket TryEvaluateTicket(string code);

        void DestroyTicket(string code);
    }
}
