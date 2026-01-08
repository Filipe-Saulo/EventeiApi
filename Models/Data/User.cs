using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace EventeiApi.Models.Data
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public DateTime? LastLogin { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public ICollection<Address> Addresses { get; set; }
        public ICollection<Document> Documents { get; set; }
        public ICollection<Event> OrganizedEvents { get; set; }
        public ICollection<EventOrder> Orders { get; set; }
        public ICollection<UserTicket> Tickets { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
