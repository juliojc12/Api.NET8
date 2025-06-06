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

            modelBuilder.Entity<Cliente>( entity =>
            {
                entity.HasKey( c => c.Id );
                entity.Property( c => c.Nome )
                      .IsRequired()
                      .HasMaxLength( 250 );
                entity.Property( c => c.Telefone )
                      .HasMaxLength( 50 );

                entity.OwnsOne( c => c.Email, email =>
                {
                    email.HasIndex( e => e.Value )
                         .IsUnique();
                } );

                entity.OwnsOne( c => c.Endereco );
            } );
        }
    }
}