using System;
using System.Collections.Generic;

namespace EventeiApi.Models.Data
{
    public class Coupon
    {
        public int CouponId { get; set; }
        public string Code { get; set; }
        public string DiscountType { get; set; } // PERCENTAGE ou FIXED
        public decimal DiscountValue { get; set; }
        public int MaxUses { get; set; }
        public int UsedCount { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<EventOrder> EventOrders { get; set; }
    }
}
