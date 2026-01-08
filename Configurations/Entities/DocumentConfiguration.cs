using EventeiApi.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventeiApi.Configurations.Entities
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable("document");

            builder.HasKey(d => d.DocumentId);
            builder.Property(d => d.DocumentId).HasColumnName("document_id").ValueGeneratedOnAdd();
            builder.Property(d => d.DocumentType).HasColumnName("document_type").IsRequired();
            builder.Property(d => d.DocumentNumber).HasColumnName("document_number").IsRequired();
            builder.Property(d => d.IssuedCountry).HasColumnName("issued_country");
            builder.Property(d => d.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(d => d.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAddOrUpdate();

            builder.HasOne(d => d.User)
                   .WithMany(u => u.Documents)
                   .HasForeignKey(d => d.UserId)
                   .HasConstraintName("fk_document_user")
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
