using System.ComponentModel.DataAnnotations;

namespace ApiFinanceiro.Dtos
{
    public class PedidoDto
    {
        [Required]
        public required Guid UsuarioId { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "O pedido precisa ter pelo menos um item")]
        public required List<ItemPedidoDto> Itens { get; set; }
    }
}
