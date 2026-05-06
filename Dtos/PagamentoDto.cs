using System.ComponentModel.DataAnnotations;

namespace ApiFinanceiro.Dtos
{
    public class PagamentoDto
    {
        [Required]
        public required Guid PedidoId { get; set; }

        [Required]
        public required string FormaPagamento { get; set; }

        public string Status { get; set; } = "Pendente";
    }
}
