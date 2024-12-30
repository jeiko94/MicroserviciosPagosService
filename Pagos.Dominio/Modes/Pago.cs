namespace Pagos.Dominio.Modes
{
    public class Pago
    {
        public int Id { get; set; }
        
        //El pedido al que se asocia este pago (pedidoId viene del servicio Pedidos)
        public int PedidoId { get; set; }
        public decimal Monto { get; set; }
        public EstadoPedido Estado { get; set; } = EstadoPedido.Pendiente;
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        //Se pueden agregar mas campos como MetodoPagoId, ReferenciaTransaccion, etc.
    }
}
