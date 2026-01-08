using EventeiApi.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventeiApi.Configurations.Entities
{
    public class EventCategoryConfiguration : IEntityTypeConfiguration<EventCategory>
    {
        public void Configure(EntityTypeBuilder<EventCategory> builder)
        {
            builder.ToTable("event_category");

            builder.HasKey(ec => ec.CategoryId);
            builder.Property(ec => ec.CategoryId).HasColumnName("category_id").ValueGeneratedOnAdd();
            builder.Property(ec => ec.Name).HasColumnName("name").IsRequired();
            builder.Property(ec => ec.IsAdult).HasColumnName("is_adult").IsRequired();
            builder.Property(ec => ec.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(ec => ec.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAddOrUpdate();

            builder.HasMany(ec => ec.Events)
                   .WithOne(e => e.Category)
                   .HasForeignKey(e => e.CategoryId)
                   .HasConstraintName("fk_event_category")
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
