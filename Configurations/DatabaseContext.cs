



using Api.Configurations.Entities;
using Api.Models.Data;
using Eventei_Api.Models.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Configurations
{
    public class DatabaseContext : IdentityDbContext<User, IdentityRole, string>
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Event> Evento { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Renomeia a coluna Id para user_id na tabela User
            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .HasColumnName("user_id");

            // Outros ajustes e configurações de outras entidades
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }
    }

    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            var conn = config.GetConnectionString("DefaultConnection");

            // Detecção automática da versão do MySQL
            var serverVersion = ServerVersion.AutoDetect(conn);
            optionsBuilder.UseMySql(conn, serverVersion);  // Alterar para MySQL

            return new DatabaseContext(optionsBuilder.Options);
        }
    }

}
