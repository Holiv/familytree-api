using FamilyTree.DTOs;

namespace FamilyTree.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioResponseDto?> CriarAsync(UsuarioCreateDto dto);
        Task<bool> VincularTokenAsync(int usuarioId, string tokenValue);
        Task<UsuarioResponseDto?> ObterPorIdAsync(int id);
        Task<IEnumerable<UsuarioDto>> ObterTodosAsync();
    }
}
