using System;
using System.Collections.Generic;

namespace FamilyTree.DTOs
{
    public class PedidoCreateDto
    {
        public int UsuarioId { get; set; }
        public List<ItemPedidoDto> Itens { get; set; } = new();
        public decimal Frete { get; set; } = 0m;
    }

    public class PedidoResponseDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime CriadoEm { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal Subtotal { get; set; }
        public decimal Frete { get; set; }
        public decimal Total { get; set; }
        public List<ItemPedidoDto> Itens { get; set; } = new();
    }
}