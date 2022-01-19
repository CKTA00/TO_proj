using ParkingApplication.CashSystem;

namespace ParkingApplication.DeviceInterface
{
    public interface ICashMachineObserver
    {
        void InsertCoin(AllowedDenominations denomination);
        //void PayByDebitCard(string debitCardData, int valuePLN, int valueGR);
    }
}
