using FamilyTree.Data;
using FamilyTree.DTOs;
using FamilyTree.Models;
using FamilyTree.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FamilyTree.Services
{
    public class AssinaturaService : IAssinaturaService
    {
        private readonly AppDbContext _context;

        public AssinaturaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CancelarAsync(int id)
        {
            var assinatura = await _context.Assinaturas.FindAsync(id) ?? throw new KeyNotFoundException("Assinatura não encontrada.");

            assinatura.Ativa = false;
            assinatura.Fim = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<AssinaturaResponseDto> CriarAsync(AssinaturaCreateDto dto)
        {
            var usuario = await _context.Usuarios.FindAsync(dto.UsuarioId) ?? throw new KeyNotFoundException("Usuário não encontrado.");

            var assinatura = new Assinatura
            {
                UsuarioId = dto.UsuarioId,
                Plano = dto.Plano,
                Inicio = DateTime.UtcNow,
                Ativa = true,
                MetodoPagamento = dto.MetodoPagamento
            };
            _context.Assinaturas.Add(assinatura);
            await _context.SaveChangesAsync();

            return Map(assinatura);
        }

        public async Task<AssinaturaResponseDto?> ObterPorIdAsync(int id)
        {
            var assinatura = await _context.Assinaturas.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
            return assinatura == null ? null : Map(assinatura);
        }

        public async Task<IEnumerable<AssinaturaResponseDto>> ObterPorUsuarioAsync(int usuarioId)
        {
            var assinaturas = await _context.Assinaturas
                .Where(a => a.UsuarioId == usuarioId)
                .OrderByDescending(a => a.Inicio)
                .ToListAsync();

            return assinaturas.Select(Map);
        }

        private static AssinaturaResponseDto Map(Assinatura a) => new()
        {
            Id = a.Id,
            UsuarioId = a.UsuarioId,
            Plano = a.Plano,
            Inicio = a.Inicio,
            Fim = a.Fim,
            Ativa = a.Ativa,
            TransacaoId = a.TransacaoId,
            MetodoPagamento = a.MetodoPagamento
        };
    }
}