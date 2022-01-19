namespace ParkingApplication.DeviceInterface
{
    public interface ICodeScannerObserver
    {
        void CodeScanned(string code);
    }
}
