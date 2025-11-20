namespace FamilyTree.DTOs
{
    public class ProdutoCreateDto
    {
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
        public string? Categoria { get; set; }
    }

    public class ProdutoResponseDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }
        public bool Ativo { get; set; }
        public int Estoque { get; set; }
        public string? Categoria { get; set; }
    }
}