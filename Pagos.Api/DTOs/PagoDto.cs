namespace Pagos.Api.DTOs
{
    public class PagoDto
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public decimal Monto { get; set; }
        public string? Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
