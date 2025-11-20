using FamilyTree.DTOs;

namespace FamilyTree.Services.Interfaces
{
    public interface IAssinaturaService
    {
        Task<AssinaturaResponseDto> CriarAsync(AssinaturaCreateDto dto);
        Task<AssinaturaResponseDto?> ObterPorIdAsync(int id);
        Task<IEnumerable<AssinaturaResponseDto>> ObterPorUsuarioAsync(int usuarioId);
        Task<bool> CancelarAsync(int id);
    }
}