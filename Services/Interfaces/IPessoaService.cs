using FamilyTree.DTOs;

namespace FamilyTree.Services.Interfaces
{
    public interface IPessoaService
    {
        Task<PessoaResponseDto?> CriarAsync(PessoaUpsertDto dto);
        Task<PessoaResponseDto?> AtualizarAsync(int id, PessoaUpsertDto dto);
        Task<PessoaResponseDto?> ObterPorIdAsync(int id);
        Task<IEnumerable<PessoaResponseDto>> ObterTodosAsync();
        Task<IEnumerable<FilhoDto>> ObterFilhosAsync(int id);
        Task<IEnumerable<PessoaResumoDto>> ObterIrmaosAsync(int id);
        Task<object?> ObterSubArvoreAsync(int id);
    }
}
