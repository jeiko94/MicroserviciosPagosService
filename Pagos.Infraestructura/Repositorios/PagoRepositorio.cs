using Microsoft.EntityFrameworkCore;
using Pagos.Aplicacion.Respositorios;
using Pagos.Dominio.Modes;
using Pagos.Infraestructura.Data;

namespace Pagos.Infraestructura.Repositorios
{
    public class PagoRepositorio : IPagoRepositorio
    {
        private readonly PagosDbContext _context;

        public PagoRepositorio(PagosDbContext context)
        {
            _context = context;
        }

        public async Task CrearAsync(Pago pago)
        {
            _context.Pagos.Add(pago);
            await _context.SaveChangesAsync();
        }
        public async Task<Pago> ObternerPorIdAsync(int id)
        {
            return await _context.Pagos.FindAsync(id);
        }
        public async Task<IEnumerable<Pago>> ObtenerPorPedidoAsync(int pedidoId)
        {
            return await _context.Pagos
                .Where(p => p.PedidoId == pedidoId)
                .ToListAsync();
        }
        public async Task ActualizarAsync(Pago pago)
        {
            _context.Pagos.Update(pago);
            await _context.SaveChangesAsync();
        }
        public async Task EliminarAsync(int id)
        {
            var pago = await ObternerPorIdAsync(id);

            if (pago != null)
            {
                _context.Pagos.Remove(pago);
                await _context.SaveChangesAsync();
            }
        }
    }
}
