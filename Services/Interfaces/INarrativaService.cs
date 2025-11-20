using FamilyTree.DTOs;

namespace FamilyTree.Services.Interfaces
{
    public interface INarrativaService
    {
        Task<NarrativaResponseDto> CriarAsync(NarrativaCreateDto dto);
        Task<NarrativaResponseDto?> ObterPorIdAsync(int id);
    }
}