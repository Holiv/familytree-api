using FamilyTree.DTOs;
using FamilyTree.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        public async Task<ActionResult<PessoaResponseDto>> CreatePessoa(PessoaUpsertDto dto)
        {
            var result = await _pessoas.CriarAsync(dto);
            return CreatedAtAction(nameof(GetPessoaById), new { id = result?.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PessoaResponseDto>> UpdatePessoa(int id, PessoaUpsertDto dto)
        {
            var result = await _pessoas.AtualizarAsync(id, dto);
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PessoaResponseDto>> GetPessoaById(int id)
        {
            var result = await _pessoas.ObterPorIdAsync(id);
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
    }
}
