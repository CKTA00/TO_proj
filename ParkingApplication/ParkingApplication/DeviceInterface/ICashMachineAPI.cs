using ParkingApplication.CashSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingApplication.DeviceInterface
{
    interface ICashMachineAPI
    {
        void InsertCoin(AllowedDenominations denomination);
        void PayByDebitCard(string debitCardData, int valuePLN, int valueGR);
    }
}
