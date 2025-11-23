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

        public async Task<PessoaResponseDto?> CriarAsync(PessoaUpsertDto dto, int usuarioId)
        {
            var criador = await _context.Usuarios.FindAsync(usuarioId) ?? throw new Exception("Usuário criador não encontrado.");

            var pessoa = new Pessoa
            {
                Nome = dto.Nome,
                DataNascimento = dto.DataNascimento,
                CPF = dto.Cpf,
                PaiId = dto.PaiId,
                MaeId = dto.MaeId,
                ConjugeId = dto.ConjugeId,
                CriadorUsuarioId = criador.Id
            };

            _context.Pessoas.Add(pessoa);
            await _context.SaveChangesAsync();

            // 🔹 Gera token automaticamente se não houver usuário vinculado
            if (pessoa.UsuarioId == null)
            {
                var token = new ValidationToken
                {
                    PessoaId = pessoa.Id,
                    GeradoPorUsuarioId = usuarioId,
                    Token = Guid.NewGuid().ToString("N"),
                    DataCriacao = DateTime.UtcNow
                };

                _context.ValidationTokens.Add(token);
                await _context.SaveChangesAsync();
            }

            return await ObterPorIdAsync(pessoa.Id, usuarioId);
        }

        public async Task<PessoaResponseDto?> AtualizarAsync(int id, PessoaUpsertDto dto, int usuarioId)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);
            if (pessoa is null) return null;

            // 🔹 Garantir que apenas o criador pode atualizar
            if (pessoa.CriadorUsuarioId != usuarioId)
                throw new UnauthorizedAccessException("Você não tem permissão para atualizar esta pessoa.");

            pessoa.Nome = dto.Nome;
            pessoa.DataNascimento = dto.DataNascimento;
            pessoa.CPF = dto.Cpf;
            pessoa.PaiId = dto.PaiId;
            pessoa.MaeId = dto.MaeId;
            pessoa.ConjugeId = dto.ConjugeId;

            await _context.SaveChangesAsync();

            return await ObterPorIdAsync(pessoa.Id, usuarioId);
        }

        public async Task<PessoaResponseDto?> ObterPorIdAsync(int id, int usuarioId)
        {
            var pessoa = await _context.Pessoas
                .Include(p => p.Pai)
                .Include(p => p.Mae)
                .Include(p => p.Conjuge)
                .Include(p => p.FilhosDoPai)
                .Include(p => p.FilhosDaMae)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pessoa is null) return null;

            var response = new PessoaResponseDto
            {
                Id = pessoa.Id,
                Nome = pessoa.Nome,
                DataNascimento = pessoa.DataNascimento,
                Cpf = pessoa.CPF,
                PaiId = pessoa.PaiId,
                MaeId = pessoa.MaeId,
                ConjugeId = pessoa.ConjugeId,
                FilhosDoPai = pessoa.FilhosDoPai.Select(f => new FilhoDto { Id = f.Id, Nome = f.Nome }).ToList(),
                FilhosDaMae = pessoa.FilhosDaMae.Select(f => new FilhoDto { Id = f.Id, Nome = f.Nome }).ToList(),
            };

            // 🔹 Retorna token apenas se o usuário logado for o criador
            var token = await _context.ValidationTokens
                .FirstOrDefaultAsync(t => t.PessoaId == pessoa.Id && !t.Utilizado);

            if (token != null && pessoa.CriadorUsuarioId == usuarioId)
            {
                response.ValidationToken = token.Token;
            }

            return response;
        }

        public async Task<IEnumerable<PessoaResponseDto>> ObterTodosAsync()
        {
            var pessoas = await _context.Pessoas
                .Include(p => p.FilhosDoPai)
                .Include(p => p.FilhosDaMae)
                .ToListAsync();
            

            return pessoas.Select(p => new PessoaResponseDto
            {
                Id = p.Id,
                Nome = p.Nome,
                DataNascimento = p.DataNascimento,
                Cpf = p.CPF,
                PaiId = p.PaiId,
                MaeId = p.MaeId,
                ConjugeId = p.ConjugeId,
                FilhosDoPai = p.FilhosDoPai.Select(f => new FilhoDto { Id = f.Id, Nome = f.Nome }).ToList(),
                FilhosDaMae = p.FilhosDaMae.Select(f => new FilhoDto { Id = f.Id, Nome = f.Nome }).ToList(),
                
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

        public async Task<IEnumerable<FilhoDto>> ObterFilhosDoPaiAsync(int id)
        {
            var filhos = await _context.Pessoas
                .Where(p => p.PaiId == id)
                .ToListAsync();

            return filhos.Select(f => new FilhoDto
            {
                Id = f.Id,
                Nome = f.Nome
            });
        }

        public async Task<IEnumerable<FilhoDto>> ObterFilhosDaMaeAsync(int id)
        {
            var filhos = await _context.Pessoas
                .Where(p => p.MaeId == id)
                .ToListAsync();

            return filhos.Select(f => new FilhoDto
            {
                Id = f.Id,
                Nome = f.Nome
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
                .Include(p => p.FilhosDoPai)
                .Include(p => p.FilhosDaMae)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pessoa is null) return null;

            return new
            {
                Pessoa = pessoa.Nome,
                FilhosDoPai = pessoa.FilhosDoPai.Select(f => new { f.Id, f.Nome }),
                FilhosDaMae = pessoa.FilhosDaMae.Select(f => new { f.Id, f.Nome })
            };
        }

        public async Task<bool> DeletarAsync(int pessoaId)
        {
            var pessoa = await _context.Pessoas.FindAsync(pessoaId);

            if (pessoa == null)
                throw new Exception("Pessoa não encontrada.");

            _context.Pessoas.Remove(pessoa);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
