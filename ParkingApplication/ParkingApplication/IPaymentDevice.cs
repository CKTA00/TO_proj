using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingApplication
{
    interface IPaymentDevice
    {
        void disposeChange();
        void accpetButtonPressed();
        void cancelButtonPressed();
        void addObserverToButton(ButtonKey key, IGuiEventListener observer); //delegat
    }
}
