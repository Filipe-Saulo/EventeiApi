using EventeiApi.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventeiApi.Configurations.Entities
{
    public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> builder)
        {
            builder.ToTable("coupon");

            builder.HasKey(c => c.CouponId);
            builder.Property(c => c.CouponId).HasColumnName("coupon_id").ValueGeneratedOnAdd();
            builder.Property(c => c.Code).HasColumnName("code").IsRequired();
            builder.HasIndex(c => c.Code).IsUnique(); // unique
            builder.Property(c => c.DiscountType).HasColumnName("discount_type").IsRequired();
            builder.Property(c => c.DiscountValue).HasColumnName("discount_value").HasColumnType("decimal(10,2)").IsRequired();
            builder.Property(c => c.MaxUses).HasColumnName("max_uses").IsRequired();
            builder.Property(c => c.UsedCount).HasColumnName("used_count").IsRequired();
            builder.Property(c => c.ExpiresAt).HasColumnName("expires_at");
            builder.Property(c => c.IsActive).HasColumnName("is_active").IsRequired();
            builder.Property(c => c.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
