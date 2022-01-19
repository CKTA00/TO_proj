using ParkingApplication.Util;
using System;
using System.Collections.Generic;

namespace ParkingApplication.ParkingSystem
{
    class TicketDatabase
    {
        Dictionary<string, Ticket> tickets;
        ICodeGenerator generator;
        int counter;
        int placesMax = 50;

        public TicketDatabase(ICodeGenerator generator, int placesMax, List<Ticket> tickets = null)
        {
            this.tickets = new Dictionary<string, Ticket>();
            counter = 0;
            if(tickets!=null)
            {
                foreach(Ticket t in tickets)
                {
                    this.tickets.Add(t.Code, t);
                }
                counter++;
            }
            
            this.placesMax = placesMax;
            this.generator = generator;
        }

        public Ticket TryAddTicket()
        {
            if (counter + 1 <= placesMax)
            {
                Ticket t = new Ticket(generator.Generate(), DateTime.Now);
                tickets.Add(t.Code, t);
                counter++;
                return t;
            }
            else
            {
                throw new NoPlaceLeftException();
            }
        }

        public Ticket TryEvaluateTicket(string code)
        {
            if(tickets.ContainsKey(code) && tickets[code].Code == code) //whatever, chcek both
            {
                return tickets[code];
            }
            else
            {
                throw new InvalidTicketCodeException();
            }
        }

        public void DestroyTicket(string code)
        {
            counter--;
            if (tickets.ContainsKey(code))
            {
                tickets.Remove(code);
            }
            else
            {
                //nothing
            }
        }
    }
}
