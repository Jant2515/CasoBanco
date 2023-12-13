using Microsoft.EntityFrameworkCore;

namespace BancoPromericaCaso.Models
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> opciones)
            : base(opciones)
        {
        }

        public DbSet<usuario> usuario { get; set; }
        public DbSet<citas> citas { get; set; }
        public DbSet<clientes> clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<clientes>(clientes =>
            {
                clientes.HasKey(x => x.IdCliente);
                clientes.Property(x => x.NombreCompleto)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<usuario>(usuario =>
            {
                usuario.HasKey(x => x.IdUsuario);
                usuario.Property(x => x.Nombres)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });


            modelBuilder.Entity<citas>()
                .HasOne(x => x.usuario)
                .WithMany(s => s.citas)
                .HasForeignKey(f => f.IdUsuario);
            modelBuilder.Entity<citas>()
                .HasOne(u => u.clientes)
                .WithMany(s => s.citas)
                .HasForeignKey(f => f.IdCliente);
        }
    }
}
