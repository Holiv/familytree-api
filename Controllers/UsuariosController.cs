using FamilyTree.DTOs;
using FamilyTree.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FamilyTree.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarios;

        public UsuariosController(IUsuarioService usuarios)
        {
            _usuarios = usuarios;
        }

        /// <summary>
        /// Cria um novo usuário. 
        /// - Se informado ValidationToken, vincula a Pessoa existente ao novo usuário.
        /// - Se não informado, cria uma nova Pessoa junto com o usuário.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<UsuarioResponseDto>> CreateUsuario(UsuarioCreateDto dto)
        {
            try
            {
                var result = await _usuarios.CriarAsync(dto);
                return CreatedAtAction(nameof(GetUsuarioById), new { id = result?.Id }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Vincula um ValidationToken a um usuário já existente.
        /// Permite que o usuário herde uma Pessoa pré-cadastrada e seus registros.
        /// </summary>
        [HttpPut("{id}/token")]
        public async Task<ActionResult> VincularToken(int id, [FromBody] string token)
        {
            var result = await _usuarios.VincularTokenAsync(id, token);
            if (!result) return BadRequest(new { message = "Token inválido ou já utilizado." });
            return Ok(new { message = "Token vinculado com sucesso." });
        }

        /// <summary>
        /// Obtém um usuário por ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioResponseDto>> GetUsuarioById(int id)
        {
            var result = await _usuarios.ObterPorIdAsync(id);
            if (result is null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Lista todos os usuários.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDto>>> GetUsuarios()
        {
            var results = await _usuarios.ObterTodosAsync();
            return Ok(results);
        }
    }
}
