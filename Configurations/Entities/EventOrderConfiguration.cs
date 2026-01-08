using EventeiApi.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventeiApi.Configurations.Entities
{
    public class EventOrderConfiguration : IEntityTypeConfiguration<EventOrder>
    {
        public void Configure(EntityTypeBuilder<EventOrder> builder)
        {
            builder.ToTable("event_order");

            builder.HasKey(o => o.OrderId);
            builder.Property(o => o.OrderId).HasColumnName("order_id").ValueGeneratedOnAdd();
            builder.Property(o => o.TotalAmount).HasColumnName("total_amount").HasColumnType("decimal(10,2)").IsRequired();
            builder.Property(o => o.DiscountAmount).HasColumnName("discount_amount").HasColumnType("decimal(10,2)").IsRequired();
            builder.Property(o => o.Status).HasColumnName("status").IsRequired();
            builder.Property(o => o.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(o => o.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAddOrUpdate();

            builder.HasOne(o => o.User)
                   .WithMany(u => u.Orders)
                   .HasForeignKey(o => o.UserId)
                   .HasConstraintName("fk_event_order_user")
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Event)
                   .WithMany(e => e.Orders)
                   .HasForeignKey(o => o.EventId)
                   .HasConstraintName("fk_event_order_event")
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.Coupon)
                   .WithMany(c => c.EventOrders)
                   .HasForeignKey(o => o.CouponId)
                   .HasConstraintName("fk_event_order_coupon")
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
