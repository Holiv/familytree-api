namespace FamilyTree.Models
{
    public class Pedido
    {
        public int Id { get; set; }

        // Quem comprou
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        // Dados do pedido
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Pendente"; // Pendente, Pago, Enviado, Concluido, Cancelado

        // Totais
        public decimal Subtotal { get; set; }
        public decimal Frete { get; set; }
        public decimal Total { get; set; }

        // Itens
        public ICollection<ItemPedido> Itens { get; set; } = new List<ItemPedido>();
    }
}