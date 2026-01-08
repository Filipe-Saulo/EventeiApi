using EventeiApi.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventeiApi.Configurations.Entities
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("address");

            builder.HasKey(a => a.AddressId);
            builder.Property(a => a.AddressId).HasColumnName("address_id").ValueGeneratedOnAdd();
            builder.Property(a => a.Street).HasColumnName("street").IsRequired();
            builder.Property(a => a.StreetNumber).HasColumnName("street_number").IsRequired();
            builder.Property(a => a.Latitude).HasColumnName("latitude");
            builder.Property(a => a.Longitude).HasColumnName("longitude");
            builder.Property(a => a.City).HasColumnName("city");
            builder.Property(a => a.State).HasColumnName("state");
            builder.Property(a => a.Neighborhood).HasColumnName("neighborhood");
            builder.Property(a => a.ZipCode).HasColumnName("zipcod");
            builder.Property(a => a.Country).HasColumnName("country");
            builder.Property(a => a.Complement).HasColumnName("complement");
            builder.Property(a => a.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(a => a.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAddOrUpdate();

            builder.HasOne(a => a.User)
                   .WithMany(u => u.Addresses)
                   .HasForeignKey(a => a.UserId)
                   .HasConstraintName("fk_address_user")
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(a => a.UserId).IsUnique(); // unique constraint
        }
    }
}
