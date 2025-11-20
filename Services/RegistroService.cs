using FamilyTree.Data;
using FamilyTree.DTOs;
using FamilyTree.Models;
using FamilyTree.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FamilyTree.Services
{
    public class RegistroService : IRegistroService
    {
        private readonly AppDbContext _context;

        public RegistroService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<RegistroResponseDto> CriarAsync(RegistroUpsertDto dto, int criadorId)
        {
            _ = await _context.Usuarios.FindAsync(criadorId) ?? throw new KeyNotFoundException("Usuário criador não encontrado.");
            var registro = new Registro
            {
                Data = dto.Data,
                Legenda = dto.Legenda,
                FotoPath = dto.FotoPath,
                CriadorId = criadorId
            };

            _context.Registros.Add(registro);
            await _context.SaveChangesAsync();

            foreach (var pessoaId in dto.PessoasIds)
            {
                _context.RegistrosPessoa.Add(new RegistroPessoa
                {
                    RegistroId = registro.Id,
                    PessoaId = pessoaId
                });
            }

            await _context.SaveChangesAsync();

            return await ObterPorIdAsync(registro.Id) ?? throw new Exception("Erro ao criar registro.");
        }

        public async Task<RegistroResponseDto?> ObterPorIdAsync(int id)
        {
            var registro = await _context.Registros
                .Include(r => r.Criador)
                .Include(r => r.RegistroPessoas)
                .ThenInclude(rp => rp.Pessoa)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (registro is null) return null;

            return new RegistroResponseDto
            {
                Id = registro.Id,
                Data = registro.Data,
                Legenda = registro.Legenda,
                FotoPath = registro.FotoPath,
                CriadorId = registro.CriadorId,
                CriadorEmail = registro.Criador.Email,
                PessoasEnvolvidas = registro.RegistroPessoas.Select(rp => new PessoaResumoDto
                {
                    Id = rp.Pessoa.Id,
                    Nome = rp.Pessoa.Nome
                }).ToList()
            };
        }

        public async Task<IEnumerable<RegistroResponseDto>> ObterTodosAsync()
        {
            var registros = await _context.Registros
                .Include(r => r.Criador)
                .Include(r => r.RegistroPessoas)
                .ThenInclude(rp => rp.Pessoa)
                .OrderByDescending(r => r.Data)
                .ToListAsync();

            return registros.Select(r => new RegistroResponseDto
            {
                Id = r.Id,
                Data = r.Data,
                Legenda = r.Legenda,
                FotoPath = r.FotoPath,
                CriadorId = r.CriadorId,
                CriadorEmail = r.Criador.Email,
                PessoasEnvolvidas = r.RegistroPessoas.Select(rp => new PessoaResumoDto
                {
                    Id = rp.Pessoa.Id,
                    Nome = rp.Pessoa.Nome
                }).ToList()
            });
        }
    }
}
