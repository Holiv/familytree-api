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

        [HttpGet("pessoa/{pessoaId}")]
        public async Task<ActionResult<IEnumerable<RegistroResponseDto>>> ObterRegistrosPorPessoaId(int pessoaId)
        {
            var registros = await _registros.ObterPorPessoaIdAsync(pessoaId);
            if (registros is null || !registros.Any()) return NotFound(new { Message = "Nenhum registro encontrado para a pessoa especificada." });
            return Ok(registros);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RegistroResponseDto>> AtualizarRegistro(int id, RegistroUpsertDto dto)
        {
            if (dto is null)
            {
                return BadRequest(new { Message = "Dados de atualização inválidos." });
            }

            var updatedRegistro = await _registros.AtualizarAsync(id, dto);
            if (updatedRegistro is null) return NotFound(new { Message = "Registro não encontrado para atualização." });
            return Ok(updatedRegistro);
        }

    }
}
