﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingApplication.DeviceInterface
{
    public interface ICodeScannerObserver
    {
        void CodeScanned(string code);
    }
}
