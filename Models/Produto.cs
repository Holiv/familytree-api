using System;

namespace FamilyTree.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }
        public bool Ativo { get; set; } = true;
        public int Estoque { get; set; } = 0; // quantidade disponível
        public string? Categoria { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    }
}