using FamilyTree.DTOs;
using FamilyTree.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FamilyTree.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrosController : ControllerBase
    {
        private readonly IRegistroService _registros;

        public RegistrosController(IRegistroService registros)
        {
            _registros = registros;
        }

        [HttpPost("{criadorId}")]
        public async Task<ActionResult<RegistroResponseDto>> CriarRegistro(int criadorId, RegistroUpsertDto dto)
        {
            var result = await _registros.CriarAsync(dto, criadorId);
            return CreatedAtAction(nameof(ObterRegistroPorId), new { id = result.Id }, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RegistroResponseDto>> ObterRegistroPorId(int id)
        {
            var result = await _registros.ObterPorIdAsync(id);
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegistroResponseDto>>> ObterRegistros()
        {
            var results = await _registros.ObterTodosAsync();
            return Ok(results);
        }
    }
}
