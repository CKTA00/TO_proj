namespace ParkingApplication.DeviceInterface
{
    interface IScannerAPI
    {
        void AddScannerObserver(ICodeScannerObserver observer);
        void RemoveScannerObserver(ICodeScannerObserver observer);
        void AnnounceScanAll(string code);
        void AnnounceScan(string code,ICodeScannerObserver observer);
    }
}
