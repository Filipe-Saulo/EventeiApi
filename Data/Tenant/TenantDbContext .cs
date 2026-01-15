
using EventeiApi.Data.Tenant.Configurations;
using EventeiApi.Models.Tenant.cs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EventeiApi.Data.Tenant
{
    public class TenantDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public TenantDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventCategory> EventCategories { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<EventOrder> EventOrders { get; set; }
        public DbSet<UserTicket> UserTickets { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Renomeia a coluna Id para user_id na tabela User
            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .HasColumnName("user_id");

            // Outros ajustes e configurações de outras entidades
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new EventConfiguration());
            modelBuilder.ApplyConfiguration(new EventCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new PhotoConfiguration());
            modelBuilder.ApplyConfiguration(new TicketConfiguration());
            modelBuilder.ApplyConfiguration(new EventOrderConfiguration());
            modelBuilder.ApplyConfiguration(new UserTicketConfiguration());
            modelBuilder.ApplyConfiguration(new DocumentConfiguration());
            modelBuilder.ApplyConfiguration(new AddressConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new CouponConfiguration());
        }
    }   
}
