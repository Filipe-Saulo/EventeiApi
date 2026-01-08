using System;
using System.Collections.Generic;

namespace EventeiApi.Models.Data
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public int EventId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation
        public Event Event { get; set; }
        public ICollection<UserTicket> UserTickets { get; set; }
    }
}
