namespace ApiFinanceiro.Models
{
    public class ItemPedido
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required Guid PedidoId { get; set; }
        public required Guid ProdutoId { get; set; }
        public required int Quantidade { get; set; }
        public required decimal PrecoUnitario { get; set; }
    }
}
