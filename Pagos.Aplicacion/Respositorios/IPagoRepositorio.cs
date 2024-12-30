using Pagos.Dominio.Modes;

namespace Pagos.Aplicacion.Respositorios
{
    public interface IPagoRepositorio
    {
        Task CrearAsync(Pago pago);
        Task<Pago> ObternerPorIdAsync(int id);
        Task<IEnumerable<Pago>> ObtenerPorPedidoAsync(int pedidoId);
        Task ActualizarAsync(Pago pago);
        Task EliminarAsync(int id);
    }
}
