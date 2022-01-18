using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingApplication.DeviceInterface;
using ParkingApplication.ParkingSystem;
using ParkingApplication.Premium;

namespace ParkingApplication.Devices
{
    class RegisterDevice : Device
    {
        Ticket currentTicket;
        PremiumUser currentUser;

        public RegisterDevice(ISimpleDialog initDisplay, PremiumDatabase premiumDB) : base(initDisplay, premiumDB)
        {
        }

        public override void ButtonPressed(ButtonKey key)
        {
            switch(key)
            {
                case ButtonKey.ACCEPT_BUTTON:

                break;
            }
        }

        public override void CardSwiped(string code)
        {
            try
            {
                currentUser = premiumDB.FindUserByCode(code);
            }
            catch(InvalidPremiumUserCodeException){
                display.ShowMessage("Nie rozpoznano karty.");
                return;
            }
            display.ShowMessage("Witaj stały kliencie.");
            if (currentUser.ExpiryDate > DateTime.Now)
            {
                display.ShowMessage("Twoja kart straciła ważność.");
            }
            else
            {
                display.ShowMessage("Twoja karta straci ważność "+currentUser.ExpiryDate.ToString());
            }
            display.ShowMessage("Kliknij ACCEPT .");
        }

        public override void Main()
        {
            display.ShowMessage("Witaj użytkowniku. Zeskanuj bilet aby zapłacić. Naciśnij accept aby wyrobić kartę stałego klienta. Naciśnij cancel jeśli zgubiłeś bilet.");
        }

        
    }
}
