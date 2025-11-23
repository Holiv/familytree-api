using FamilyTree.DTOs;

namespace FamilyTree.Services.Interfaces
{
    public interface IPessoaService
    {
        Task<PessoaResponseDto?> CriarAsync(PessoaUpsertDto dto, int usuarioId);
        Task<PessoaResponseDto?> AtualizarAsync(int id, PessoaUpsertDto dto, int usuarioId);
        Task<PessoaResponseDto?> ObterPorIdAsync(int id, int usuarioId);
        Task<IEnumerable<PessoaResponseDto>> ObterTodosAsync();
        Task<IEnumerable<FilhoDto>> ObterFilhosAsync(int id);
        Task<IEnumerable<FilhoDto>> ObterFilhosDoPaiAsync(int id);
        Task<IEnumerable<FilhoDto>> ObterFilhosDaMaeAsync(int id);
        Task<IEnumerable<PessoaResumoDto>> ObterIrmaosAsync(int id);
        Task<object?> ObterSubArvoreAsync(int id);
        Task<bool> DeletarAsync(int pessoaId);
    }
}
