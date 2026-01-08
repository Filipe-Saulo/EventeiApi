using System;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;

namespace EventeiApi.Models.Data
{
    public class EventOrder
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public int? CouponId { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public string Status { get; set; } // PENDING, PAID, CANCELED

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation
        public User User { get; set; }
        public Event Event { get; set; }
        public Coupon Coupon { get; set; }
        public ICollection<UserTicket> UserTickets { get; set; }
    }
}
