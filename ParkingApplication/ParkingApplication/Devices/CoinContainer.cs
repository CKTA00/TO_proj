using ParkingApplication.CashSystem;
using ParkingApplication.DeviceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingApplication.Devices
{
    class CoinContainer : ICashMachineObserver
    {
        ISimpleDialog display;
        IPaymentDone ctx;
        ICashMachineOutput cashOutput;
        Dictionary<AllowedDenominations, int> coins;
        Dictionary<AllowedDenominations, int> box;
        bool closeBox;
        int total;
        public CoinContainer(ICashMachineOutput cashBox, int initialCoins=100)
        {
            coins = new Dictionary<AllowedDenominations, int>();
            box = new Dictionary<AllowedDenominations, int>();

            this.cashOutput = cashBox;
            closeBox = false;
            foreach(AllowedDenominations den in (AllowedDenominations[]) Enum.GetValues(typeof(AllowedDenominations)))
            {
                coins.Add(den, initialCoins);
                box.Add(den, 0);
            }
        }

        public void SetContext(IPaymentDone ctx, ISimpleDialog display)
        {
            this.display = display;
            this.ctx = ctx;
        }

        public void InsertCoin(AllowedDenominations denomination)
        {
            if (!closeBox)
            {
                cashOutput.ThrowCoin(denomination);
            }
            else
            {
                box[denomination]++;
                Eval();
            }
        }

        void Eval()
        {
            int value = 0;
            

            foreach (AllowedDenominations den in box.Keys)
            {
                value += (int)den * box[den];
            }
            value = total - value;

            if (value < 0)
            {
                display.ShowMessage("Reszta: " + -value / 100 + " zł " + -value % 100 + " gr");
                foreach(AllowedDenominations den in box.Keys)
                {
                    coins[den] += box[den];
                    box[den] = 0;
                }
                Rest(-value);
                closeBox = false;
            }
            else
            {
                display.ShowMessage("Do wrzucenia: " + value / 100 + " zł " + value % 100 + " gr");
            }

        }

        private void Rest(int value)
        {
            List<AllowedDenominations> denoms = new List<AllowedDenominations>(coins.Keys);
            denoms.Sort();

            foreach (AllowedDenominations den in denoms)
            {
                int reqCoins = value / (int)den;
                if (reqCoins < coins[den])
                {
                    value -= (int)den * reqCoins;
                    cashOutput.ThrowCoins(den, reqCoins);
                    coins[den] -= reqCoins;
                }
                else if (reqCoins > coins[den])
                {
                    value -= (int)den * coins[den];
                    cashOutput.ThrowCoins(den, coins[den]);
                    coins[den] = 0;
                }
                
                if (value == 0) break;
                if(den == AllowedDenominations.M10gr && value > 0)//move outside the loop
                {
                    throw new NoMoneyInBankException();
                }
            }
            display.ShowMessage("Reszta została wypłacona"); //move to Device
        }

        public void RequestValue(int totalInGr)
        {
            total = totalInGr;
            closeBox = true;
            Eval();
        }

        public void CancelPayment()
        {
            closeBox = false;
            foreach (AllowedDenominations den in box.Keys)
            {
                cashOutput.ThrowCoins(den, box[den]);
                box[den] = 0;
            }
            total = 0;
        }
    }
}
