using System.ComponentModel.DataAnnotations;

namespace ApiFinanceiro.Dtos
{
    public class PagamentoStatusDto
    {
        [Required]
        public required string Status { get; set; }
    }
}
