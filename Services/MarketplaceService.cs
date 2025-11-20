using FamilyTree.Data;
using FamilyTree.DTOs;
using FamilyTree.Models;
using FamilyTree.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FamilyTree.Services
{
    public class MarketplaceService : IMarketplaceService
    {
        private readonly AppDbContext _context;
        public MarketplaceService(AppDbContext context)
        {
            _context = context;
        }

        //Pedidos
        public async Task<PedidoResponseDto> CriarPedidoAsync(PedidoCreateDto dto)
        {
            var usuario = await _context.Usuarios.FindAsync(dto.UsuarioId) ?? throw new Exception("Usuário não encontrado.");

            var pedido = new Pedido
            {
                UsuarioId = dto.UsuarioId,
                Status = "Pendente",
                Frete = dto.Frete,  
            };

            decimal subtotal = 0m;
            foreach (var item in dto.Itens)
            {
                var produto = await _context.Produtos.FindAsync(item.ProdutoId) ?? throw new KeyNotFoundException($"Produto com ID {item.ProdutoId} não encontrado.");

                var itemPedido = new ItemPedido
                {
                    Pedido = pedido,
                    ProdutoId = produto.Id,
                    Produto = produto,
                    PrecoUnitario = produto.Preco,
                    Quantidade = item.Quantidade,
                };

                pedido.Itens.Add(itemPedido);
                subtotal += itemPedido.TotalItem;
            }

            pedido.Subtotal = subtotal;
            pedido.Total = subtotal + pedido.Frete;

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            return MapPedido(pedido);
        }

        public async Task<PedidoResponseDto?> ObterPedidoPorIdAsync(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Itens)
                .ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(p => p.Id == id);
            return pedido == null ? null : MapPedido(pedido);
        }

        public async Task<bool> AlterarStatusPedidoAsync(int id, string status)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null) return false;

            pedido.Status = status;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<PedidoResponseDto>> ObterPedidosAsync()
        {
            return await _context.Pedidos
                .Include(p => p.Itens)
                .ThenInclude(i => i.Produto)
                .Select(p => MapPedido(p))
                .ToListAsync();
        }

        private static PedidoResponseDto MapPedido(Pedido pedido)
        {
            return new PedidoResponseDto
            {
                Id = pedido.Id,
                UsuarioId = pedido.UsuarioId,
                CriadoEm = pedido.CriadoEm,
                Status = pedido.Status,
                Subtotal = pedido.Subtotal,
                Frete = pedido.Frete,
                Total = pedido.Total,
                Itens = pedido.Itens.Select(item => new ItemPedidoDto
                {
                    ProdutoId = item.ProdutoId,
                    Quantidade = item.Quantidade
                }).ToList()
            };
        }

        //Produtos
        public async Task<bool> AtualizarProdutoAsync(int id, ProdutoCreateDto dto)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null) return false;

            produto.Nome = dto.Nome;
            produto.Descricao = dto.Descricao;
            produto.Preco = dto.Preco;
            produto.Estoque = dto.Estoque;
            produto.Categoria = dto.Categoria;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ProdutoResponseDto> CriarProdutoAsync(ProdutoCreateDto dto)
        {
            var produto = new Produto
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                Preco = dto.Preco,
                Estoque = dto.Estoque,
                Categoria = dto.Categoria,
                Ativo = true
            };
            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return MapProduto(produto);
        }

        public async Task<bool> DesativarProdutoAsync(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null) return false;

            produto.Ativo = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<ProdutoResponseDto?> ObterProdutoPorIdAsync(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto is null) return null;

            return MapProduto(produto);
            
        }

        public async Task<IEnumerable<ProdutoResponseDto>> ObterProdutosAsync()
        {
            var produtos = await _context.Produtos
            .Where(p => p.Ativo)
            .ToListAsync();
            return produtos.Select(MapProduto);
        }   

        public static ProdutoResponseDto MapProduto(Produto produto)
        {
            return new ProdutoResponseDto
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco,
                Ativo = produto.Ativo,
                Estoque = produto.Estoque,
                Categoria = produto.Categoria
            };
        }
    }
}