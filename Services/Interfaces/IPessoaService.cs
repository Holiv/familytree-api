using FamilyTree.DTOs;

namespace FamilyTree.Services.Interfaces
{
    public interface IPessoaService
    {
        Task<PessoaResponseDto?> CriarAsync(PessoaUpsertDto dto);
        Task<PessoaResponseDto?> AtualizarAsync(int id, PessoaUpsertDto dto);
        Task<PessoaResponseDto?> ObterPorIdAsync(int id);
        Task<IEnumerable<PessoaResponseDto>> ObterTodosAsync();

        // 🔹 Métodos de filhos
        Task<IEnumerable<FilhoDto>> ObterFilhosAsync(int id);          // genérico (pai ou mãe)
        Task<IEnumerable<FilhoDto>> ObterFilhosDoPaiAsync(int id);     // apenas filhos vinculados pelo pai
        Task<IEnumerable<FilhoDto>> ObterFilhosDaMaeAsync(int id);     // apenas filhos vinculados pela mãe

        // 🔹 Outros relacionamentos
        Task<IEnumerable<PessoaResumoDto>> ObterIrmaosAsync(int id);
        Task<object?> ObterSubArvoreAsync(int id);
    }
}
