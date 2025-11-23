using FamilyTree.DTOs;
using FamilyTree.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FamilyTree.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoasController : ControllerBase
    {
        private readonly IPessoaService _pessoas;

        public PessoasController(IPessoaService pessoas)
        {
            _pessoas = pessoas;
        }

        // 🔹 Helper para obter o usuário logado
        private int GetUsuarioId(int usuarioId)
        {
            //  🔹 Implementar lógica real de autenticação aqui
            // var claim = User.FindFirst(ClaimTypes.NameIdentifier) ?? throw new UnauthorizedAccessException("Usuário não autenticado.");
            // return int.Parse(claim.Value);
            return usuarioId;
        }

        [HttpPost("{usuarioId}")]
        public async Task<ActionResult<PessoaResponseDto>> CreatePessoa(PessoaUpsertDto dto, int usuarioId)
        {
            var uId = GetUsuarioId(usuarioId);
            var result = await _pessoas.CriarAsync(dto, uId);
            return CreatedAtAction(nameof(GetPessoaById), new { id = result?.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PessoaResponseDto>> UpdatePessoa(int id, PessoaUpsertDto dto)
        {
            var usuarioId = GetUsuarioId(id);
            var result = await _pessoas.AtualizarAsync(id, dto, usuarioId);
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpGet("{id}/{usuarioId}")]
        public async Task<ActionResult<PessoaResponseDto>> GetPessoaById(int id, int usuarioId)
        {
            var uId = GetUsuarioId(usuarioId);
            var result = await _pessoas.ObterPorIdAsync(id, uId);
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PessoaResponseDto>>> GetPessoas()
        {
            var results = await _pessoas.ObterTodosAsync();
            return Ok(results);
        }

        [HttpGet("{id}/filhos")]
        public async Task<ActionResult<IEnumerable<FilhoDto>>> GetFilhos(int id)
        {
            var results = await _pessoas.ObterFilhosAsync(id);
            return Ok(results);
        }

        [HttpGet("{id}/filhos/pai")]
        public async Task<ActionResult<IEnumerable<FilhoDto>>> GetFilhosDoPai(int id)
        {
            var results = await _pessoas.ObterFilhosDoPaiAsync(id);
            return Ok(results);
        }

        [HttpGet("{id}/filhos/mae")]
        public async Task<ActionResult<IEnumerable<FilhoDto>>> GetFilhosDaMae(int id)
        {
            var results = await _pessoas.ObterFilhosDaMaeAsync(id);
            return Ok(results);
        }

        [HttpGet("{id}/irmaos")]
        public async Task<ActionResult<IEnumerable<PessoaResumoDto>>> GetIrmaos(int id)
        {
            var results = await _pessoas.ObterIrmaosAsync(id);
            return Ok(results);
        }

        [HttpGet("{id}/subarvore")]
        public async Task<ActionResult<object?>> GetSubArvore(int id)
        {
            var result = await _pessoas.ObterSubArvoreAsync(id);
            return Ok(result);
        }

        [HttpDelete("{pessoaId}")]
        public async Task<IActionResult> DeletePessoa(int pessoaId)
        {
            var success = await _pessoas.DeletarAsync(pessoaId);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
