using ParkingApplication.CashSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingApplication.DeviceInterface
{
    interface ICashMachineOutput
    {
        void ThrowCoin(AllowedDenominations den);
        void ThrowBox();
    }
}
