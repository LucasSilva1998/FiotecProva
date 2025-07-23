using FiotecProva.Application.Dtos.Request;
using FiotecProva.Application.Dtos.Response;
using FiotecProva.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FiotecProva.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicosController : ControllerBase
    {
        private readonly IMedicoService _medicoService;

        public MedicosController(IMedicoService medicoService)
        {
            _medicoService = medicoService;
        }

        /// <summary>
        /// Retorna uma lista de médicos cadastrados no sistema.
        /// </summary>
        [HttpGet("Listar-Medicos")]
        public async Task<ActionResult<IEnumerable<MedicoResponse>>> Listar()
        {
            var medicos = await _medicoService.ListarAsync();
            return Ok(medicos);
        }

        /// <summary>
        /// Obtém um médico cadastrado no sistema por ID.
        /// </summary>
        [HttpGet("{id:int}/Obter-medico")]
        public async Task<ActionResult<MedicoResponse>> ObterPorId(int id)
        {
            try
            {
                var medico = await _medicoService.ObterPorIdAsync(id);
                return Ok(medico);
            }
            catch (Exception ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Gera o cadastro de um novo médico no sistema.
        /// </summary>
        [HttpPost("Criar")]
        public async Task<ActionResult<Guid>> Criar([FromBody] MedicoRequest request)
        {
            try
            {
                var medicoId = await _medicoService.CriarAsync(request);
                return CreatedAtAction(nameof(ObterPorId), new { id = medicoId }, medicoId);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza os dados de um médico existente no sistema.
        /// </summary>
        [HttpPut("{id:int}/Atualizar")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] MedicoRequest request)
        {
            try
            {
                await _medicoService.AtualizarAsync(id, request);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// remover um médico existente no sistema.
        /// </summary>
        [HttpDelete("{id:int}/Remover")]
        public async Task<IActionResult> Remover(int id)
        {
            try
            {
                await _medicoService.RemoverAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }
    }
}
