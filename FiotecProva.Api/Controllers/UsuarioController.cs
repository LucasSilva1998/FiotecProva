using FiotecProva.Application.Dtos.Request;
using FiotecProva.Application.Dtos.Response;
using FiotecProva.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FiotecProva.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Cria um novo usuário no sistema.
        /// </summary>
        [HttpPost("Criar")]
        public async Task<ActionResult<UsuarioResponse>> Criar([FromBody] UsuarioRequest request)
        {
            try
            {
                var usuario = await _usuarioService.CriarAsync(request);
                return CreatedAtAction(nameof(ObterPorId), new { id = usuario.Id }, usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Obtém um usuário cadastrado no sistema por ID.
        /// </summary>
        [HttpGet("{id:int}/Obter-usuario")]
        public async Task<ActionResult<UsuarioResponse>> ObterPorId(int id)
        {
            var usuario = await _usuarioService.ObterPorIdAsync(id);
            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        /// <summary>
        /// Retorna uma lista de usuários cadastrados no sistema.
        /// </summary>
        [HttpGet("Listar")]
        public async Task<ActionResult<IEnumerable<UsuarioResponse>>> Listar()
        {
            var usuarios = await _usuarioService.ListarAsync();
            return Ok(usuarios);
        }
    }
}

