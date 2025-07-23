using FiotecProva.Application.Dtos.Request;
using FiotecProva.Application.Dtos.Response;
using FiotecProva.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FiotecProva.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly IPacienteService _pacienteService;

        public PacientesController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        /// <summary>
        /// Retorna uma lista de pacientes cadastrados no sistema.
        /// </summary>
        [HttpGet("Listar-Pacientes")]
        public async Task<ActionResult<IEnumerable<PacienteResponse>>> Listar()
        {
            var pacientes = await _pacienteService.ListarAsync();
            return Ok(pacientes);
        }

        /// <summary>
        /// Retorna um paciente cadastrado no sistema por ID.
        /// </summary>
        [HttpGet("{id:int}/Obter-paciente")]
        public async Task<ActionResult<PacienteResponse>> ObterPorId(int id)
        {
            try
            {
                var paciente = await _pacienteService.ObterPorIdAsync(id);
                return Ok(paciente);
            }
            catch (Exception ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Cria um novo paciente no sistema.
        /// </summary>
        [HttpPost("Criar")]
        public async Task<ActionResult<Guid>> Criar([FromBody] PacienteRequest request)
        {
            try
            {
                var pacienteId = await _pacienteService.CadastrarAsync(request);
                return CreatedAtAction(nameof(ObterPorId), new { id = pacienteId }, pacienteId);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza um paciente com cadastro existente no sistema.
        /// </summary>
        [HttpPut("{id:int}/Atualizar")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] PacienteRequest request)
        {
            try
            {
                await _pacienteService.AtualizarAsync(id, request);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Remove um paciente com cadastro existente no sistema.
        /// </summary>
        [HttpDelete("{id:int}/Remover")]
        public async Task<IActionResult> Remover(int id)
        {
            try
            {
                await _pacienteService.RemoverAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }
    }
}