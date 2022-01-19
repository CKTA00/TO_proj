namespace ParkingApplication.Premium
{
    interface IPremiumDatabase
    {
        PremiumUser RegisterPremiumUser(string plateNumber);

        PremiumUser GetPremiumUser(string plateNumber, string code);

        PremiumUser FindUserByCode(string code);
    }
}
