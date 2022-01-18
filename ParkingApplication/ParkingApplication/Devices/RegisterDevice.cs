using System;
using ParkingApplication.CashSystem;
using ParkingApplication.DeviceInterface;
using ParkingApplication.ParkingSystem;
using ParkingApplication.Premium;

namespace ParkingApplication.Devices
{
    class RegisterDevice : Device, ICodeScannerObserver, IPaymentDone
    {
        Ticket currentTicket;
        PremiumUser currentUser;
        CoinContainer bank;
        IPriceStrategy ticketPrice;
        IPriceStrategy premiumPrice;

        internal CoinContainer Bank { get => bank; }

        public RegisterDevice(ISimpleDialog initDisplay, TicketDatabase normalDB, TicketDatabase handicappedDB, PremiumDatabase premiumDB, CoinContainer bank, IPriceStrategy ticketPrice, IPriceStrategy premiumPrice) 
            : base(initDisplay, normalDB, handicappedDB, premiumDB)
        {
            this.ticketPrice = ticketPrice;
            this.premiumPrice = premiumPrice;
            this.bank = bank;
        }

        public override void Main()
        {
            display.ShowMessage("Witaj użytkowniku. Zeskanuj bilet aby zapłacić. Naciśnij accept aby wyrobić kartę stałego klienta. Naciśnij cancel jeśli zgubiłeś bilet.");
        }

        public override void ButtonPressed(ButtonKey key)
        {
            switch(key)
            {
                case ButtonKey.ACCEPT_BUTTON:

                    break;
                case ButtonKey.CANCEL_BUTTON:
                    bank.CancelPayment();
                    display.ShowMessage("Płatnośc została anulowana.");
                    break;
            }
        }

        public override void CardSwiped(string code)
        {
            try
            {
                currentUser = premiumDB.FindUserByCode(code);
            }
            catch(InvalidPremiumUserCodeException){
                display.ShowMessage("Nie rozpoznano karty.");
                return;
            }
            display.ShowMessage("Witaj stały kliencie.");
            if (currentUser.ExpiryDate > DateTime.Now)
            {
                display.ShowMessage("Twoja kart straciła ważność.");
            }
            else
            {
                display.ShowMessage("Twoja karta straci ważność "+currentUser.ExpiryDate.ToString());
            }
            display.ShowMessage("Na ile miesięcy chcesz przedłużyć ważność: ");
            int extend = 0;
            if(!int.TryParse(display.ReadString(),out extend)){
                display.ShowMessage("Proces nie udany, przybliż kartę jeszcze raz.");
                currentUser = null;
                return;
            }
        }

        public void PaymentDone()
        {
            if (currentUser != null)
            {
                display.ShowMessage("Dziękujemy za zakup premium!");
                currentUser = null;
            }
            else if(currentTicket != null)
            {
                currentTicket.Realize();
                currentTicket = null;
                display.ShowMessage("Udaj się do bramy wyjazdowej w przeciągu 15 minut. Zaskanuj tam swój bilet.");
            }
        }

        public void CodeScanned(string code)
        {
            currentUser = null;
            Ticket ticket;
            TicketDatabase sourceDB;
            try
            {
                ticket = normalTicketsDB.TryEvaluateTicket(code);
                sourceDB = normalTicketsDB;
            }
            catch (InvalidTicketCodeException)
            {
                try
                {
                    ticket = handicappedTicketsDB.TryEvaluateTicket(code);
                    sourceDB = handicappedTicketsDB;
                }
                catch (InvalidTicketCodeException e)
                {
                    display.ShowMessage("Twój bilet jest nieważny! e: " + e.Message);
                    return;
                }
            }

            if (ticket.IsPaid)
            {
                TimeSpan timeLeft = ticket.PaymentTime - DateTime.Now + new TimeSpan(0, 15, 0);
                if (timeLeft>TimeSpan.Zero)
                {
                    display.ShowMessage("Ten bilet jest już opłacony. Zostało ci " + timeLeft.TotalMinutes + " na wyjazd.");
                    return;
                }
                else
                {
                    ticket.Underpaid();
                }
            }
            currentTicket = ticket;
            bank.RequestValue(ticketPrice.CalculatePriceInGr(DateTime.Now - ticket.EntranceTime));
        }
    }
}
