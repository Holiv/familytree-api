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

        [HttpPost]
        public async Task<ActionResult<UsuarioResponseDto>> CreateUsuario(UsuarioCreateDto dto)
        {
            var result = await _usuarios.CriarAsync(dto);
            return CreatedAtAction(nameof(GetUsuarioById), new { id = result.Id }, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioResponseDto>> GetUsuarioById(int id)
        {
            var result = await _usuarios.ObterPorIdAsync(id);
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioResponseDto>>> GetUsuarios()
        {
            var results = await _usuarios.ObterTodosAsync();
            return Ok(results);
        }
    }
}
