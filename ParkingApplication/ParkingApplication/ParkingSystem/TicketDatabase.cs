﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (counter + 1 > placesMax)
            {
                Ticket t = new Ticket("c", DateTime.Now);
                tickets.Add(t.Code, t);
                counter++;
                return t;
            }
            else
            {
                throw new NoPlaceLeftException();
            }
        }

        public bool EvaluateTicket(string code)
        {
            return tickets.ContainsKey(code) && tickets[code].Code == code; //whatever, chcek both
        }
    }
}
