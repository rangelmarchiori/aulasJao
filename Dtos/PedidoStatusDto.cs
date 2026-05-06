using System.ComponentModel.DataAnnotations;

namespace ApiFinanceiro.Dtos
{
    public class PedidoStatusDto
    {
        [Required]
        public required string Status { get; set; }
    }
}
