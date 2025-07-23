using FiotecProva.Application.Dtos.Request;
using FiotecProva.Application.Dtos.Response;
using FiotecProva.Application.Interfaces;
using FiotecProva.Domain.Paginations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FiotecProva.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultasController : ControllerBase
    {
        private readonly IConsultaService _consultaService;

        public ConsultasController(IConsultaService consultaService)
        {
            _consultaService = consultaService;
        }

        /// <summary>
        /// Retorna uma lista de consultas cadastradas no sistema (paginada).
        /// </summary>
        [HttpGet("Listar-Consultas")]
        public async Task<ActionResult<PagedList<ConsultaResponse>>> Listar([FromQuery] PaginationFilter filtro)
        {
            var resultadoPaginado = await _consultaService.ListarAsync(filtro);
            return Ok(resultadoPaginado);
        }


        /// <summary>
        /// Retorna uma consulta cadastrada no sistema por id.
        /// </summary>
        [HttpGet("{id:int}/Obter-consulta")]
        public async Task<ActionResult<ConsultaResponse>> ObterPorId(int id)
        {
            try
            {
                var consulta = await _consultaService.ObterPorIdAsync(id);
                return Ok(consulta);
            }
            catch (Exception ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Agendar uma nova consulta no sistema.
        /// </summary>
        [HttpPost("Agendar")]
        public async Task<ActionResult<int>> Agendar([FromBody] ConsultaRequest request)
        {
            try
            {
                var consultaId = await _consultaService.AgendarAsync(request);
                return CreatedAtAction(nameof(ObterPorId), new { id = consultaId }, consultaId);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Cancelar uma consulta existente no sistema.
        /// </summary>
        [HttpPut("{id:int}/Cancelar")]
        public async Task<IActionResult> Cancelar(int id, [FromBody] ConsultaCancelamentoRequest request)
        {
            try
            {
                await _consultaService.CancelarAsync(id, request);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }
    }
}