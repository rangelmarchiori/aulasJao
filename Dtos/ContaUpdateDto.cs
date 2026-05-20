using System.ComponentModel.DataAnnotations;

namespace ApiFinanceiro.Dtos
{
    public class ContaUpdateDto : ContaDto
    {
        [Required]
        public DateOnly DataRecebimento { get; set; }
    }
}
