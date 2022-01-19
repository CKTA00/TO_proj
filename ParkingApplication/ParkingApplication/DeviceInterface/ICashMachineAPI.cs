using ParkingApplication.CashSystem;

namespace ParkingApplication.DeviceInterface
{
    interface ICashMachineAPI
    {
        void AddCashMachineObserver(ICashMachineObserver o);
        void RemoveCashMachineObserver(ICashMachineObserver o);
        void InsertCoin(AllowedDenominations den, ICashMachineObserver o);
    }
}
