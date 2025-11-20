using FamilyTree.Data;
using FamilyTree.DTOs;
using FamilyTree.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FamilyTree.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarketplaceController : ControllerBase
    {
        private readonly AppDbContext _context;
        public MarketplaceController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("produtos")]
        public async Task<ActionResult<ProdutoResponseDto>> CriarProduto([FromBody] ProdutoCreateDto dto)
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
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObterProdutoPorId), new { id = produto.Id }, MapProduto(produto));
        }

        [HttpGet("produtos/{id}")]
        public async Task<ActionResult<ProdutoResponseDto>> ObterProdutoPorId(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto is null) return NotFound();

            return Ok(MapProduto(produto));
        }

        [HttpGet("produtos")]
        public async Task<ActionResult<IEnumerable<ProdutoResponseDto>>> ObterProdutos()
        {
            var produtos = await _context.Produtos.Where(p => p.Ativo).ToListAsync();
            return Ok(produtos.Select(MapProduto));
        }

        [HttpPut("produtos/{id}")]
        public async Task<IActionResult> AtualizarProduto(int id, [FromBody] ProdutoCreateDto dto)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto is null) return NotFound();

            produto.Nome = dto.Nome;
            produto.Descricao = dto.Descricao;
            produto.Preco = dto.Preco;
            produto.Estoque = dto.Estoque;
            produto.Categoria = dto.Categoria;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("produtos/{id}")]
        public async Task<IActionResult> DeletarProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto is null) return NotFound();

            produto.Ativo = false;
            await _context.SaveChangesAsync();
            return NoContent();
        }

                // ---------------- PEDIDOS ----------------
        [HttpPost("pedidos")]
        public async Task<ActionResult<PedidoResponseDto>> CriarPedido([FromBody] PedidoCreateDto dto)
        {
            var usuario = await _context.Usuarios.FindAsync(dto.UsuarioId);
            if (usuario is null) return NotFound("Usuário não encontrado.");

            var pedido = new Pedido
            {
                UsuarioId = dto.UsuarioId,
                Status = "Pendente",
                Frete = dto.Frete
            };

            decimal subtotal = 0m;
            foreach (var item in dto.Itens)
            {
                var produto = await _context.Produtos.FindAsync(item.ProdutoId);
                if (produto is null || !produto.Ativo) return BadRequest($"Produto {item.ProdutoId} inválido.");

                var novoItem = new ItemPedido
                {
                    Pedido = pedido,
                    ProdutoId = produto.Id,
                    Produto = produto,
                    PrecoUnitario = produto.Preco,
                    Quantidade = item.Quantidade
                };

                pedido.Itens.Add(novoItem);
                subtotal += produto.Preco * item.Quantidade;
            }

            pedido.Subtotal = subtotal;
            pedido.Total = subtotal + pedido.Frete;

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(ObterPedidoPorId), new { id = pedido.Id }, MapPedido(pedido));
        }

        [HttpGet("pedidos/{id}")]
        public async Task<ActionResult<PedidoResponseDto>> ObterPedidoPorId(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Itens)
                .ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido is null) return NotFound();

            return Ok(MapPedido(pedido));
        }

        [HttpGet("pedidos")]
        public async Task<ActionResult<IEnumerable<PedidoResponseDto>>> ObterPedidos()
        {
            var pedidos = await _context.Pedidos
                .Include(p => p.Itens)
                .ThenInclude(i => i.Produto)
                .OrderByDescending(p => p.CriadoEm)
                .ToListAsync();

            return Ok(pedidos.Select(MapPedido));
        }

        [HttpPost("pedidos/{id}/status")]
        public async Task<IActionResult> AlterarStatus(int id, [FromBody] string status)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido is null) return NotFound();

            pedido.Status = status;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // ---------------- MAPS ----------------
        private static ProdutoResponseDto MapProduto(Produto p)
        {
            return new ProdutoResponseDto
            {
                Id = p.Id,
                Nome = p.Nome,
                Descricao = p.Descricao,
                Preco = p.Preco,
                Ativo = p.Ativo,
                Estoque = p.Estoque,
                Categoria = p.Categoria
            };
        }

        private static PedidoResponseDto MapPedido(Pedido p)
        {
            return new PedidoResponseDto
            {
                Id = p.Id,
                UsuarioId = p.UsuarioId,
                CriadoEm = p.CriadoEm,
                Status = p.Status,
                Subtotal = p.Subtotal,
                Frete = p.Frete,
                Total = p.Total,
                Itens = p.Itens.Select(i => new ItemPedidoDto
                {
                    ProdutoId = i.ProdutoId,
                    Quantidade = i.Quantidade
                }).ToList()
            };
        }
    }
}