namespace Pagos.Api.DTOs
{
    public class IniciarPagoDto
    {
        public int PedidoId { get; set; }
        public decimal Monto { get; set; }
    }
}
