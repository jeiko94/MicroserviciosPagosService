using Microsoft.EntityFrameworkCore;
using Pagos.Dominio.Modes;

namespace Pagos.Infraestructura.Data
{
    public class PagosDbContext : DbContext
    {
        public PagosDbContext(DbContextOptions<PagosDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Pago> Pagos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
