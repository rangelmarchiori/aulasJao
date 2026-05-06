namespace ApiFinanceiro.Models
{
    public class Pedido
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Data { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Recebido";
        public required Guid UsuarioId { get; set; }
        public List<ItemPedido> Itens { get; set; } = new();
    }
}
