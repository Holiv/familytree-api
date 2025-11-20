using FamilyTree.DTOs;

namespace FamilyTree.Services.Interfaces
{
    public interface IMarketplaceService
    {
        // Produtos
        Task<ProdutoResponseDto> CriarProdutoAsync(ProdutoCreateDto dto);
        Task<ProdutoResponseDto?> ObterProdutoPorIdAsync(int id);
        Task<IEnumerable<ProdutoResponseDto>> ObterProdutosAsync();
        Task<bool> AtualizarProdutoAsync(int id, ProdutoCreateDto dto);
        Task<bool> DesativarProdutoAsync(int id);

        // Pedidos
        Task<PedidoResponseDto> CriarPedidoAsync(PedidoCreateDto dto);
        Task<PedidoResponseDto?> ObterPedidoPorIdAsync(int id);
        Task<IEnumerable<PedidoResponseDto>> ObterPedidosAsync();
        Task<bool> AlterarStatusPedidoAsync(int id, string status);
    }
}
