﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingApplication.DeviceInterface
{
    interface ISimpleDialog
    {
        void ShowMessage(String msg);
        String ReadString();
    }
}
