using System.Collections.Generic;

namespace FamilyTree.DTOs
{
    // MVP simples para catálogo de produtos
    public class ProdutoDto
    {
        public int Id { get; set; } // opcional para retorno
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }
        public bool Ativo { get; set; } = true;
    }

    public class ProdutoListResponseDto
    {
        public List<ProdutoDto> Produtos { get; set; } = new();
    }
}