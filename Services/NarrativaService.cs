using System.Collections.Generic;
using FamilyTree.Data;
using FamilyTree.DTOs;
using FamilyTree.Models;
using FamilyTree.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FamilyTree.Services
{
    public class NarrativaService : INarrativaService
    {
        private readonly AppDbContext _context;

        public NarrativaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<NarrativaResponseDto> CriarAsync(NarrativaCreateDto dto)
        {
            var criador = await _context.Usuarios.FindAsync(dto.CriadorUsuarioId) ?? throw new KeyNotFoundException("Criador não encontrado.");
            var pessoa = await _context.Pessoas.FindAsync(dto.PessoaId) ?? throw new KeyNotFoundException("Pessoa não encontrada.");

            var registro = new Registro
            {
                Data = dto.Data,
                Legenda = dto.Titulo,
                FotoPath = null,
                CriadorId = dto.CriadorUsuarioId,
            };

            _context.Registros.Add(registro);
            await _context.SaveChangesAsync();

            _context.RegistrosPessoa.Add(new RegistroPessoa
            {
                RegistroId = registro.Id,
                PessoaId = pessoa.Id
            });

            await _context.SaveChangesAsync();

            return new NarrativaResponseDto
            {
                Id = registro.Id,
                CriadorUsuarioId = dto.CriadorUsuarioId,
                PessoaId = pessoa.Id,
                Titulo = dto.Titulo,
                Texto = dto.Texto, // ainda não persistido (MVP)
                Data = dto.Data,
                CriadorEmail = criador.Email,
                PessoaNome = pessoa.Nome
            };
        }

        public async Task<NarrativaResponseDto?> ObterPorIdAsync(int id)
        {
            var registro = await _context.Registros
                .Include(r => r.Criador)
                .Include(r => r.RegistroPessoas)
                .ThenInclude(rp => rp.Pessoa)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (registro is null) return null;

            var pessoa = registro.RegistroPessoas.FirstOrDefault()?.Pessoa;

            return new NarrativaResponseDto
            {
                Id = registro.Id,
                CriadorUsuarioId = registro.CriadorId,
                PessoaId = pessoa?.Id ?? 0,
                Titulo = registro.Legenda,
                Texto = string.Empty, // MVP
                Data = registro.Data,
                CriadorEmail = registro.Criador.Email,
                PessoaNome = pessoa?.Nome
            };
        }
    }
}
