namespace ParkingApplication.DeviceInterface
{
    public interface IButtonObserver
    {
        void ButtonPressed(ButtonKey key);
    }
}