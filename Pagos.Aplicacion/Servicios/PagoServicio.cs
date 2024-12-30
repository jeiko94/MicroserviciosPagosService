using Pagos.Aplicacion.Respositorios;
using Pagos.Dominio.Modes;

namespace Pagos.Aplicacion.Servicios
{
    public class PagoServicio
    {
        private readonly IPagoRepositorio _pagoRepositorio;

        public PagoServicio(IPagoRepositorio pagoRepositorio)
        {
            _pagoRepositorio = pagoRepositorio;
        }

        //Iniciar un nuevo pago para un Pedido, en estado Pendiente.
        public async Task<int> IniciarPagoAsync(int pedidoId, decimal monto)
        {
            //Realizar validaciones de negocio, como verificar que el pedido exista, que no tenga un pago pendiente, etc.
            var pago = new Pago
            {
                PedidoId = pedidoId,
                Monto = monto,
                Estado = EstadoPedido.Pendiente,
                FechaCreacion = DateTime.UtcNow
            };

            await _pagoRepositorio.CrearAsync(pago);

            return pago.Id; //Ingluye el id a al pago
        }

        //Marcar un pago como Exitoso (o fallido) una vez procesado (Simula una pasarela de pago)
        public async Task MarcarResultadoPagadoAsync(int pagoId, bool exitoso)
        {
            var pago = await _pagoRepositorio.ObternerPorIdAsync(pagoId);

            if (pago == null)
                throw new InvalidOperationException("Pago no encontrado");

            if (pago.Estado != EstadoPedido.Pendiente)
                throw new InvalidOperationException("Solo un pago pendiente se puede modificar.");

            pago.Estado = exitoso ? EstadoPedido.Exitoso : EstadoPedido.Fallido;

            await _pagoRepositorio.ActualizarAsync(pago);

            // Si exitoso = true, luego podríamos notificar al Servicio Pedidos -> "MarcarPagado"
        }

        public async Task<Pago> ObtenerPagoAsync(int id)
        {
            return await _pagoRepositorio.ObternerPorIdAsync(id);
        }

        public async Task<IEnumerable<Pago>> ObtenerPagosPorPedidoAsync(int pedidoId)
        {
            return await _pagoRepositorio.ObtenerPorPedidoAsync(pedidoId);
        }

        public async Task EliminarPagoAsync(int id)
        {
            await _pagoRepositorio.EliminarAsync(id);
        }
    }
}
