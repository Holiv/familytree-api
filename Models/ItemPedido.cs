namespace FamilyTree.Models
{
    public class ItemPedido
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; } = null!;

        public int ProdutoId { get; set; }
        public Produto Produto { get; set; } = null!;

        public decimal PrecoUnitario { get; set; }
        public int Quantidade { get; set; }

        public decimal TotalItem => PrecoUnitario * Quantidade;
    }
}