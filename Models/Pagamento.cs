namespace ApiFinanceiro.Models
{
    public class Pagamento
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required Guid PedidoId { get; set; }
        public required string FormaPagamento { get; set; }
        public string Status { get; set; } = "Pendente";
    }
}
