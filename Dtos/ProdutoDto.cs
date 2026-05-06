using System.ComponentModel.DataAnnotations;

namespace ApiFinanceiro.Dtos
{
    public class ProdutoDto
    {
        [Required]
        public required string Nome { get; set; }

        [Required]
        public required string Descricao { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preco deve ser maior que zero")]
        public required decimal Preco { get; set; }

        [Required]
        public required Guid CategoriaId { get; set; }

        public bool Disponivel { get; set; } = true;
    }
}
