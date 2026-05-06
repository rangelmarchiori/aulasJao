using System.ComponentModel.DataAnnotations;

namespace ApiFinanceiro.Dtos
{
    public class CategoriaProdutoDto
    {
        [Required]
        public required string Nome { get; set; }
    }
}
