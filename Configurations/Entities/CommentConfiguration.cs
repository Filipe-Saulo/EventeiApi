using EventeiApi.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventeiApi.Configurations.Entities
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("comment");

            builder.HasKey(c => c.CommentId);
            builder.Property(c => c.CommentId).HasColumnName("comment_id").ValueGeneratedOnAdd();
            builder.Property(c => c.Rating).HasColumnName("rating").IsRequired();
            builder.Property(c => c.Content).HasColumnName("content").IsRequired();
            builder.Property(c => c.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasOne(c => c.User)
                   .WithMany(u => u.Comments)
                   .HasForeignKey(c => c.UserId)
                   .HasConstraintName("fk_comment_user")
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Event)
                   .WithMany(e => e.Comments)
                   .HasForeignKey(c => c.EventId)
                   .HasConstraintName("fk_comment_event")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Organizer)
                   .WithMany()
                   .HasForeignKey(c => c.OrganizerId)
                   .HasConstraintName("fk_comment_organizer")
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
