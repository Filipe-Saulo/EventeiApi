using EventeiApi.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventeiApi.Configurations.Entities
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("ticket");

            builder.HasKey(t => t.TicketId);
            builder.Property(t => t.TicketId).HasColumnName("ticket_id").ValueGeneratedOnAdd();
            builder.Property(t => t.Name).HasColumnName("name").IsRequired();
            builder.Property(t => t.Price).HasColumnName("price").HasColumnType("decimal(10,2)").IsRequired();
            builder.Property(t => t.Quantity).HasColumnName("quantity").IsRequired();
            builder.Property(t => t.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(t => t.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAddOrUpdate();

            builder.HasOne(t => t.Event)
                   .WithMany(e => e.Tickets)
                   .HasForeignKey(t => t.EventId)
                   .HasConstraintName("fk_ticket_event")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
