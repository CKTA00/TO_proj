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
        string login;
        bool transaction;
        bool userRegister;
        bool handicapped;
        int extend;

        internal CoinContainer Bank { get => bank; }

        public RegisterDevice(ISimpleDialog initDisplay, ITicketDatabase normalDB, ITicketDatabase handicappedDB, IPremiumDatabase premiumDB, CoinContainer bank, IPriceStrategy ticketPrice, IPriceStrategy premiumPrice) 
            : base(initDisplay, normalDB, handicappedDB, premiumDB)
        {
            this.ticketPrice = ticketPrice;
            this.premiumPrice = premiumPrice;
            this.bank = bank;
            transaction = false;
            userRegister = false;
        }

        public override void Main()
        {
            display.ShowMessage("Witaj użytkowniku. Zeskanuj bilet aby zapłacić. Naciśnij accept lub special aby wyrobić kartę stałego klienta (zwykła/dla niepełnosprawnych). Naciśnij cancel aby anulować transakcje.");
        }

        public override void ButtonPressed(ButtonKey key)
        {
            switch(key)
            {
                case ButtonKey.ACCEPT_BUTTON:
                    if (EndTransaction()) return;
                    handicapped = false;
                    RegisterNewUser();
                    break;
                case ButtonKey.SPECIAL_BUTTON:
                    if (EndTransaction()) return;
                    handicapped = true;
                    RegisterNewUser();
                    break;
                case ButtonKey.CANCEL_BUTTON:
                    transaction = false;
                    bank.CancelPayment();
                    currentTicket = null;
                    currentUser = null;
                    display.ShowMessage("Płatnośc została anulowana.");
                    break;
            }
        }

        bool EndTransaction()
        {
            if (transaction)
                display.ShowMessage("Zakończ poprzednią transakcje.");
            return transaction;
        }

        private void RegisterNewUser()
        {
            if (EndTransaction()) return;
            
            display.ShowMessage("Podaj numer rejestracyjny: ");
            login = display.ReadString();

            display.ShowMessage("Wyrobienie karty wraz z 3 darmowymi miesiącami kosztuje 25 zł.");
            transaction = true;
            userRegister = true;
            bank.RequestValue(2500);

        }

        public override void CardSwiped(string code)
        {
            if (EndTransaction()) return;
            try
            {
                currentUser = premiumDB.FindUserByCode(code);
            }
            catch(InvalidPremiumUserCodeException){
                display.ShowMessage("Nie rozpoznano karty.");
                return;
            }
            display.ShowMessage("Witaj stały kliencie.");
            if (currentUser.ExpiryDate <= DateTime.Now)
            {
                display.ShowMessage("Twoja kart straciła ważność.");
            }
            else
            {
                display.ShowMessage("Twoja karta straci ważność "+currentUser.ExpiryDate.ToString());
            }
            display.ShowMessage("Na ile miesięcy chcesz przedłużyć ważność: ");
            extend = 0;
            if(!int.TryParse(display.ReadString(),out extend)){
                display.ShowMessage("Proces nie udany, przybliż kartę jeszcze raz.");
                currentUser = null;
                return;
            }

            transaction = true;
            bank.RequestValue(premiumPrice.CalculatePriceInGr(new TimeSpan(30 * extend, 0, 0, 0, 0)));
        }

        public void PaymentDone()
        {
            transaction = false;
            if (userRegister)
            {
                PremiumUser u = premiumDB.RegisterPremiumUser(login);
                u.IsHandicapped = handicapped;
                display.ShowMessage("Dziękujemy za zakup premium! Oto twoja karta: "+u.Code);
            }
            else if(currentUser != null)
            {
                currentUser.Extend(new TimeSpan(30 * extend, 0, 0, 0, 0));
                display.ShowMessage("Dziękujemy za przedłużenie ważności karty.");
            }
            else if(currentTicket != null)
            {
                currentTicket.Realize();
                display.ShowMessage("Udaj się do bramy wyjazdowej w przeciągu 15 minut. Zskanuj tam swój bilet.");
            }
            currentUser = null;
            currentTicket = null;
            userRegister = false;
        }

        public void CodeScanned(string code)
        {
            if (EndTransaction()) return;
            currentUser = null;
            Ticket ticket;
            ITicketDatabase sourceDB;
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
                    display.ShowMessage("Ten bilet jest już opłacony. Zostało ci " + (int)timeLeft.TotalMinutes + " minut na wyjazd.");
                    return;
                }
                else
                {
                    ticket.Underpaid();
                }
            }
            currentTicket = ticket;
            transaction = true;
            bank.RequestValue(ticketPrice.CalculatePriceInGr(DateTime.Now - ticket.EntranceTime));
        }
    }
}
