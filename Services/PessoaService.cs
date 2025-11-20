using FamilyTree.Data;
using FamilyTree.DTOs;
using FamilyTree.Models;
using FamilyTree.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FamilyTree.Services
{
    public class PessoaService : IPessoaService
    {
        private readonly AppDbContext _context;

        public PessoaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PessoaResponseDto?> CriarAsync(PessoaUpsertDto dto)
        {
            var pessoa = new Pessoa
            {
                Nome = dto.Nome,
                DataNascimento = dto.DataNascimento,
                CPF = dto.Cpf,
                PaiId = dto.PaiId,
                MaeId = dto.MaeId,
                ConjugeId = dto.ConjugeId
            };

            _context.Pessoas.Add(pessoa);
            await _context.SaveChangesAsync();

            return await ObterPorIdAsync(pessoa.Id);
        }

        public async Task<PessoaResponseDto?> AtualizarAsync(int id, PessoaUpsertDto dto)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);
            if (pessoa is null) return null;

            pessoa.Nome = dto.Nome;
            pessoa.DataNascimento = dto.DataNascimento;
            pessoa.CPF = dto.Cpf;
            pessoa.PaiId = dto.PaiId;
            pessoa.MaeId = dto.MaeId;
            pessoa.ConjugeId = dto.ConjugeId;

            await _context.SaveChangesAsync();
            return await ObterPorIdAsync(pessoa.Id);
        }

        public async Task<PessoaResponseDto?> ObterPorIdAsync(int id)
        {
            var pessoa = await _context.Pessoas
                .Include(p => p.Pai)
                .Include(p => p.Mae)
                .Include(p => p.Conjuge)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pessoa is null) return null;

            return new PessoaResponseDto
            {
                Id = pessoa.Id,
                Nome = pessoa.Nome,
                DataNascimento = pessoa.DataNascimento,
                Cpf = pessoa.CPF,
                PaiId = pessoa.PaiId,
                MaeId = pessoa.MaeId,
                ConjugeId = pessoa.ConjugeId
            };
        }

        public async Task<IEnumerable<PessoaResponseDto>> ObterTodosAsync()
        {
            var pessoas = await _context.Pessoas.ToListAsync();
            return pessoas.Select(p => new PessoaResponseDto
            {
                Id = p.Id,
                Nome = p.Nome,
                DataNascimento = p.DataNascimento,
                Cpf = p.CPF,
                PaiId = p.PaiId,
                MaeId = p.MaeId,
                ConjugeId = p.ConjugeId
            });
        }

        public async Task<IEnumerable<FilhoDto>> ObterFilhosAsync(int id)
        {
            var filhos = await _context.Pessoas
                .Where(p => p.PaiId == id || p.MaeId == id)
                .ToListAsync();

            return filhos.Select(f => new FilhoDto
            {
                Id = f.Id,
                Nome = f.Nome,
            });
        }

        public async Task<IEnumerable<PessoaResumoDto>> ObterIrmaosAsync(int id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);
            if (pessoa is null) return Enumerable.Empty<PessoaResumoDto>();

            var irmaos = await _context.Pessoas
                .Where(p => p.Id != id && (p.PaiId == pessoa.PaiId || p.MaeId == pessoa.MaeId))
                .ToListAsync();

            return irmaos.Select(i => new PessoaResumoDto
            {
                Id = i.Id,
                Nome = i.Nome
            });
        }

        public async Task<object?> ObterSubArvoreAsync(int id)
        {
            var pessoa = await _context.Pessoas
                .Include(p => p.Filhos)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pessoa is null) return null;

            return new
            {
                Pessoa = pessoa.Nome,
                Filhos = pessoa.Filhos.Select(f => new { f.Id, f.Nome })
            };
        }
    }
}
