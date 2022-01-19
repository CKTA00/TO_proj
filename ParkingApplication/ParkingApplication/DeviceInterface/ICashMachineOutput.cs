using ParkingApplication.CashSystem;

namespace ParkingApplication.DeviceInterface
{
    interface ICashMachineOutput
    {
        void ThrowCoin(AllowedDenominations den); //throws coin from box
        void ThrowCoins(AllowedDenominations den, int amount); //throws coins from bank
    }
}
