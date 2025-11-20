using FamilyTree.DTOs;

namespace FamilyTree.Services.Interfaces
{
    public interface IRegistroService
    {
        Task<RegistroResponseDto?> CriarAsync(RegistroUpsertDto dto, int criadorId);
        Task<RegistroResponseDto?> ObterPorIdAsync(int id);
        Task<IEnumerable<RegistroResponseDto>> ObterTodosAsync();
    }
}