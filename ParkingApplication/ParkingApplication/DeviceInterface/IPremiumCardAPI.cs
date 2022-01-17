namespace ParkingApplication.DeviceInterface
{
    interface IPremiumCardAPI
    {
        void AddPremiumCardObserver(IPremiumCardObserver observer);
        void RemovePremiumCardObserver(IPremiumCardObserver observer);
        void AnnounceSwipeAll(string data);
        void AnnounceSwipe(string data, IPremiumCardObserver observer);
    }
}