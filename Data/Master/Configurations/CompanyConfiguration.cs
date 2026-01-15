using EventeiApi.Models.Master;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventeiApi.Data.Master.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("company", schema: "master");

            builder.HasKey(c => c.CompanyId);

            builder.Property(c => c.CompanyId)
                   .HasColumnName("company_id");

            builder.Property(c => c.Name)
                   .HasColumnName("name")
                   .IsRequired();

            builder.Property(c => c.SchemaName)
                   .HasColumnName("schema_name")
                   .IsRequired();

            builder.Property(c => c.IsActive)
                   .HasColumnName("is_active")
                   .HasDefaultValue(true);

            builder.Property(c => c.CreatedAt)
                   .HasColumnName("created_at")
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(c => c.UpdatedAt)
                   .HasColumnName("updated_at")
                   .HasDefaultValueSql("CURRENT_TIMESTAMP")
                   .ValueGeneratedOnAddOrUpdate();

            builder.HasIndex(c => c.SchemaName)
                   .IsUnique();
        }
    }

}
