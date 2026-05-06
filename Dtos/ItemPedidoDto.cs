using System.ComponentModel.DataAnnotations;

namespace ApiFinanceiro.Dtos
{
    public class ItemPedidoDto
    {
        [Required]
        public required Guid ProdutoId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero")]
        public required int Quantidade { get; set; }
    }
}
