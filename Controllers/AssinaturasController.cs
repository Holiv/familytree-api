using System.Reflection.Metadata.Ecma335;
using FamilyTree.DTOs;
using FamilyTree.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FamilyTree.Controllers
{
    [ApiController]
    [Route("api/assinaturas")]
    public class AssinaturasController : ControllerBase
    {
        private readonly IAssinaturaService _assinaturaService;

        public AssinaturasController(IAssinaturaService assinaturaService)
        {
            _assinaturaService = assinaturaService;
        }

        [HttpPost]
        public async Task<ActionResult<AssinaturaResponseDto>> CriarAssinatura([FromBody] AssinaturaCreateDto dto)
        {
            var assinatura = await _assinaturaService.CriarAsync(dto);
            return CreatedAtAction(nameof(ObterAssinaturaPorId), new { id = assinatura.Id }, assinatura);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AssinaturaResponseDto>> ObterAssinaturaPorId(int id)
        {
            var assinatura = await _assinaturaService.ObterPorIdAsync(id);
            if (assinatura is null) return NotFound();
            return Ok(assinatura);
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<AssinaturaResponseDto>>> ObterPorUsuario(int usuarioId)
        {
            var assinaturas = await _assinaturaService.ObterPorUsuarioAsync(usuarioId);
            return Ok(assinaturas);
        }

        [HttpDelete("{id}/cancelar")]
        public async Task<IActionResult> CancelarAssinatura(int id)
        {
            var result = await _assinaturaService.CancelarAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}