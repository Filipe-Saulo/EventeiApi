using System;
using System.Collections.Generic;

namespace EventeiApi.Models.Data
{
    public class Event
    {
        public int EventId { get; set; }
        public int AddressId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime EventDate { get; set; }
        public int TicketsQuantity { get; set; }
        public int OrganizerId { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public EventCategory Category { get; set; }
        public Address Address { get; set; }
        public User Organizer { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<EventOrder> Orders { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}
