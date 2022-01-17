using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingApplication.DeviceInterface
{
    interface ICodeScannerObserver
    {
        void CodeScanned(string code);
    }
}
