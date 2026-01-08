using EventeiApi.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventeiApi.Configurations.Entities
{
    public class UserTicketConfiguration : IEntityTypeConfiguration<UserTicket>
    {
        public void Configure(EntityTypeBuilder<UserTicket> builder)
        {
            builder.ToTable("user_ticket");

            builder.HasKey(ut => ut.UserTicketId);
            builder.Property(ut => ut.UserTicketId).HasColumnName("user_ticket_id").ValueGeneratedOnAdd();
            builder.Property(ut => ut.QrCode).HasColumnName("qr_code").IsRequired();
            builder.Property(ut => ut.CheckInAt).HasColumnName("check_in_at");
            builder.Property(ut => ut.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasOne(ut => ut.Order)
                   .WithMany(o => o.UserTickets)
                   .HasForeignKey(ut => ut.OrderId)
                   .HasConstraintName("fk_user_ticket_order")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ut => ut.Ticket)
                   .WithMany(t => t.UserTickets)
                   .HasForeignKey(ut => ut.TicketId)
                   .HasConstraintName("fk_user_ticket_ticket")
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ut => ut.User)
                   .WithMany(u => u.Tickets)
                   .HasForeignKey(ut => ut.UserId)
                   .HasConstraintName("fk_user_ticket_user")
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
