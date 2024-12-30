using Microsoft.AspNetCore.Mvc;
using Pagos.Api.DTOs;
using Pagos.Aplicacion.Servicios;

namespace Pagos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagosController : ControllerBase
    {
        private readonly PagoServicio _pagoServicio;

        public PagosController(PagoServicio pagoServicio)
        {
            _pagoServicio = pagoServicio;
        }

        [HttpPost]
        public async Task<IActionResult> IniciarPago([FromBody] IniciarPagoDto dto)
        {
            int pagoId = await _pagoServicio.IniciarPagoAsync(dto.PedidoId, dto.Monto);
            return Ok($"Pago creado con id = {pagoId}");
        }

        // POST /api/pagos/{pagoId}/resultado
        // Se asume que la pasarela de pago llama esto con un body
        [HttpPost("{pagoId}/resultado")]
        public async Task<IActionResult> MarcarResultado(int pagoId, [FromBody] MarcarResultadoDto dto)
        {
            try
            {
                await _pagoServicio.MarcarResultadoPagadoAsync(pagoId, dto.Exitoso);
                return Ok(dto.Exitoso ? "Pago exitoso" : "Pago fallido");
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPago(int pagoId)
        {
            var pago = await _pagoServicio.ObtenerPagoAsync(pagoId);

            if(pago == null)
                return NotFound("No existe el pago.");

            return Ok(new PagoDto
            {
                Id = pago.Id,
                PedidoId = pago.PedidoId,
                Monto = pago.Monto,
                Estado = pago.Estado.ToString(),
                FechaCreacion = pago.FechaCreacion
            });
        }
    }
}
