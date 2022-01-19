﻿using System;

namespace ParkingApplication.Util
{
    class GUIDGenerator : ICodeGenerator
    {
        public string Generate()
        {
            //source: StackOverflow

            long i = 1;

            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }

            string number = String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000);

            return number;
        }
    }
}
