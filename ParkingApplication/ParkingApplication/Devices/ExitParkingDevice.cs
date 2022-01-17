using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParkingApplication.ParkingSystem;
using ParkingApplication.DeviceInterface;

namespace ParkingApplication.Devices
{
    class ExitParkingDevice : GateDevice, ICodeScannerObserver
    {
        public ExitParkingDevice(ISimpleDialog initDisplay, IGateAPI machine, TicketDatabase normalTicketsDB, TicketDatabase handicappedTicketsDB) 
            : base(initDisplay, machine, normalTicketsDB, handicappedTicketsDB)
        {

        }

        public override void Main()
        {
            display.ShowMessage("Zeskanuj kod kreskowy z biletu lub użyj karty stałego klienta.");
            display.ShowMessage("Naciśnij przycisk aby wyjechać bez biletu (75 PLN).");
        }

        public void CodeScanned(string code)
        {
            Ticket ticket;
            try
            {
                ticket = normalTicketsDB.TryEvaluateTicket(code);
                //db = normalTicketsDB;
            }
            catch(InvalidTicketCodeException)
            {
                try
                {
                    ticket = handicappedTicketsDB.TryEvaluateTicket(code);
                    //db = handicappedTicketsDB;
                }
                catch (InvalidTicketCodeException e)
                {
                    display.ShowMessage("Twój bilet jest nieważny! e: " + e.Message);
                    return;
                }        
            }

            display.ShowMessage("Do zapłaty");
            //TODO: get value of ticket from datetime (using Cash strategy directly?)
        }

        public override void CardSwiped(string code)
        {
            throw new NotImplementedException();
        }

        public override void ButtonPressed(ButtonKey key)
        {
            
        }
    }
}
