using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingApplication.UserInterface
{
    class TestingConsoleUI : IPaymentDevice
    {
        ConsoleDisplay con;
        Dictionary<ButtonKey, List<IGuiEventListener>> observers;

        public TestingConsoleUI() //maybe add button list of some kind of button object?
        {
            observers = new Dictionary<ButtonKey, List<IGuiEventListener>>();
            // TODO: think of a better solution
            observers.Add(ButtonKey.ACCEPT_BUTTON, new List<IGuiEventListener>());
            observers.Add(ButtonKey.CANCEL_BUTTON, new List<IGuiEventListener>());
        }

        public void AddButtonObserver(ButtonKey key, IGuiEventListener observer)
        {
            List<IGuiEventListener> list = observers[key]; //maybe add button dictionary key here, if doesn't exist
            if(!list.Contains(observer))
            {
                list.Add(observer);
            }
        }

        public void RemoveButtonObserver(ButtonKey key, IGuiEventListener observer)
        {
            List<IGuiEventListener> list = observers[key];
            if (!list.Contains(observer))
            {
                list.Remove(observer);
            }
        }

        private void ButtonPressed(ButtonKey key)
        {
            List<IGuiEventListener> list = observers[key];
            foreach(IGuiEventListener observer in list)
            {
                observer.ButtonPressed();
            }
        }


        public void DisposeChange()
        {
            throw new NotImplementedException();
        }

        public void ShowSimulationMenu()
        {
            while(true)
            {
                con.ShowMessage("Zasymuluj działanie parkomatu. Wpisz jeden z poniższych usecaseów:");
                con.ShowMessage(">drive in");
                con.ShowMessage("\tSymuluje wjazd nowego użytkownika na parking.");
                con.ShowMessage(">drive out");
                con.ShowMessage("\tSymuluje wyjazd losowego użytkownika z parkingu.");
                con.ShowMessage(">register");
                con.ShowMessage("\tSymuluje rejestracje noweg stałego użytkownika (premium)");
                con.ShowMessage(">exit");
                con.ShowMessage("\tWyjdź.");
                string command = con.ReadString();
                if (command == "drive in") //might use chain of responsibility if enough time
                {
                    NewDriver();
                }
                else if (command == "drive out")
                {

                }
                else if (command == "register")
                {

                }
                else if (command == "exit")
                {
                    break;
                }
                else
                {
                    con.ShowMessage("Nieprawidłowe polecenie!");
                    con.ShowMessage("");
                    con.ShowMessage("");
                }
            }
        }

        private void NewDriver()
        {
            con.ShowMessage("[Automat] Witaj! Wciśnij przycisk lub pryłóż kartę stałego klienta.");
            while(true)
            {
                con.ShowMessage(">press");
                con.ShowMessage("\tNaciśnij.");
                con.ShowMessage(">card");
                con.ShowMessage("\tPrzyłóż kartę.");
                string command = con.ReadString();
                if (command == "press")
                {
                    ButtonPressed(ButtonKey.ACCEPT_BUTTON);
                    //Application calls this:
                    con.ShowMessage("[Automat] [Wysuwa bilet z kodem kreskowym "+"temp"+"]"); // TODO: generate?
                    con.ShowMessage("[Automat] Zachowaj ten bilet do wyjazdu z parkingu.");
                    //open gate
                }
                else if (command == "card")
                {
                    //TODO: get data from premium database
                    //open gate
                }
                else if (command == "exit")
                {
                    break;
                }
                else
                {
                    con.ShowMessage("Nieprawidłowe polecenie!");
                    con.ShowMessage("");
                    con.ShowMessage("");
                }
            }
        }
           
    }
}
