using EventeiApi.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventeiApi.Configurations.Entities
{
    public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.ToTable("photo");

            builder.HasKey(p => p.PhotoId);
            builder.Property(p => p.PhotoId).HasColumnName("photo_id").ValueGeneratedOnAdd();
            builder.Property(p => p.UrlPhoto).HasColumnName("url_photo").IsRequired();
            builder.Property(p => p.AfterEvent).HasColumnName("after_event").IsRequired();
            builder.Property(p => p.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(p => p.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAddOrUpdate();

            builder.HasOne(p => p.Event)
                   .WithMany(e => e.Photos)
                   .HasForeignKey(p => p.EventId)
                   .HasConstraintName("fk_photo_event")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
