using FamilyTree.DTOs;
using FamilyTree.Models;

namespace FamilyTree.Services.Interfaces
{
    public interface IRegistroService
    {
        Task<RegistroResponseDto?> CriarAsync(RegistroUpsertDto dto, int criadorId);
        Task<RegistroResponseDto?> ObterPorIdAsync(int id);
        Task<IEnumerable<RegistroResponseDto>> ObterTodosAsync();
        Task<IEnumerable<RegistroResponseDto>> ObterPorPessoaIdAsync(int pessoaId);
        Task<RegistroResponseDto?> AtualizarAsync(int id, RegistroUpsertDto dto);
        // Task<bool> DeletarAsync(int id);
    }
}