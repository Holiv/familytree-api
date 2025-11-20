using FamilyTree.DTOs;

namespace FamilyTree.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioResponseDto> CriarAsync(UsuarioCreateDto dto);
        Task<UsuarioResponseDto?> ObterPorIdAsync(int id);
        Task<IEnumerable<UsuarioResponseDto>> ObterTodosAsync();
    }
}
