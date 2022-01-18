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
        void AddCashMachineObserver(ICashMachineObserver o);
        void RemoveCashMachineObserver(ICashMachineObserver o);
        void InsertCoin(AllowedDenominations den, ICashMachineObserver o);
    }
}
