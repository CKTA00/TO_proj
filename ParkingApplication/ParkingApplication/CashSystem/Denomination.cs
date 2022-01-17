using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingApplication.CashSystem
{
    class Denomination
    {
        int valueInGr;
        int amount;
        // TODO: Add maximal capacity to send notification to money owner

        //public Denomination(int valueInGr, int amount)
        //{
        //    this.valueInGr = valueInGr;
        //    this.amount = amount;
        //}
        public Denomination( int amount)
        {
            //this.valueInGr = valueInGr;
            this.amount = amount;
        }

        //public int ValueInGr { get => valueInGr;}
        public int Amount { get => amount;}

        /// <summary>
        /// returns amount of ramaining coins
        /// </summary>
        /// <returns></returns>
        public int TakeCoinsIfPossible(int amount)
        {
            if (amount < 0)
            {
                throw new InvalidOperationException("Amount cannot be negative.");
            }

            if(this.amount - amount >= 0)
            {
                this.amount -= amount;
                return 0;
            }
            else
            {
                int rest = amount - this.amount;
                this.amount = 0;
                return rest;
            }
        }

        public void AddCoins(int amount)
        {
            if (amount < 0)
            {
                throw new InvalidOperationException("Amount cannot be negative.");
            }
            amount += amount;
        }
    }
}
