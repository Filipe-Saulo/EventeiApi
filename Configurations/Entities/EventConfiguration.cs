using EventeiApi.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventeiApi.Configurations.Entities
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("event");

            builder.HasKey(e => e.EventId);
            builder.Property(e => e.EventId).HasColumnName("event_id").ValueGeneratedOnAdd();

            builder.Property(e => e.Name).HasColumnName("name").IsRequired().HasMaxLength(255);
            builder.Property(e => e.Description).HasColumnName("description").IsRequired().HasMaxLength(1000);
            builder.Property(e => e.EventDate).HasColumnName("event_date").IsRequired();
            builder.Property(e => e.TicketsQuantity).HasColumnName("tickets_quantity").IsRequired();
            builder.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAddOrUpdate();

            // Relationships
            builder.HasOne(e => e.Category)
                   .WithMany(c => c.Events)
                   .HasForeignKey(e => e.CategoryId)
                   .HasConstraintName("fk_event_category")
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Address)
                   .WithMany()
                   .HasForeignKey(e => e.AddressId)
                   .HasConstraintName("fk_event_address")
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Organizer)
                   .WithMany(u => u.OrganizedEvents)
                   .HasForeignKey(e => e.OrganizerId)
                   .HasConstraintName("fk_event_organizer")
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}