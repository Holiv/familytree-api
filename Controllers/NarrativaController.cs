using FamilyTree.DTOs;
using FamilyTree.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FamilyTree.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NarrativaController : ControllerBase
    {
        private readonly INarrativaService _narrativas;

        public NarrativaController(INarrativaService narrativas)
        {
            _narrativas = narrativas;
        }

        [HttpPost]
        public async Task<ActionResult<NarrativaResponseDto>> Criar([FromBody] NarrativaCreateDto dto)
        {
            var result = await _narrativas.CriarAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = result.Id }, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NarrativaResponseDto>> ObterPorId(int id)
        {
            var result = await _narrativas.ObterPorIdAsync(id);
            if (result is null) return NotFound();
            return Ok(result);
        }
    }
}
