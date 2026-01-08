using System;

namespace EventeiApi.Models.Data
{
    public class UserTicket
    {
        public int UserTicketId { get; set; }
        public int OrderId { get; set; }
        public int TicketId { get; set; }
        public int UserId { get; set; }
        public string QrCode { get; set; }
        public DateTime? CheckInAt { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation
        public EventOrder Order { get; set; }
        public Ticket Ticket { get; set; }
        public User User { get; set; }
    }
}
