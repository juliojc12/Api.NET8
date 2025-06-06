using ClienteApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClienteApi.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }

        public AppDbContext( DbContextOptions<AppDbContext> options )
            : base( options )
        {
        }

        protected override void OnModelCreating( ModelBuilder modelBuilder )
        {

            modelBuilder.Entity<Cliente>( e =>
            {
                e.HasKey( c => c.Id );
                e.OwnsOne( c => c.Endereco );
                e.HasIndex( c => c.Email ).IsUnique();
            } );
        }
    }
}