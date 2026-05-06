using ApiFinanceiro.Data;
using ApiFinanceiro.Dtos;
using ApiFinanceiro.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiFinanceiro.Controllers
{
    [Route("/pedidos")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        [HttpGet]
        public ActionResult FindAll()
        {
            return Ok(TheHomeDatabase.Pedidos.Select(PedidoResponse));
        }

        [HttpGet("{id}")]
        public ActionResult FindById(Guid id)
        {
            var pedido = TheHomeDatabase.Pedidos.FirstOrDefault(p => p.Id == id);

            if (pedido is null)
            {
                return NotFound(new { mensagem = $"Pedido #{id} não encontrado" });
            }

            return Ok(PedidoResponse(pedido));
        }

        [HttpGet("usuario/{usuarioId}")]
        public ActionResult FindByUsuario(Guid usuarioId)
        {
            var usuarioExiste = TheHomeDatabase.Usuarios.Any(u => u.Id == usuarioId);

            if (!usuarioExiste)
            {
                return NotFound(new { mensagem = $"Usuário #{usuarioId} não encontrado" });
            }

            var pedidos = TheHomeDatabase.Pedidos
                .Where(p => p.UsuarioId == usuarioId)
                .Select(PedidoResponse);

            return Ok(pedidos);
        }

        [HttpPost]
        public ActionResult Create([FromBody] PedidoDto pedidoDto)
        {
            var usuario = TheHomeDatabase.Usuarios.FirstOrDefault(u => u.Id == pedidoDto.UsuarioId);

            if (usuario is null)
            {
                return BadRequest(new { mensagem = $"Usuário #{pedidoDto.UsuarioId} não encontrado" });
            }

            var pedido = new Pedido { UsuarioId = pedidoDto.UsuarioId };

            foreach (var itemDto in pedidoDto.Itens)
            {
                var produto = TheHomeDatabase.Produtos.FirstOrDefault(p => p.Id == itemDto.ProdutoId);

                if (produto is null)
                {
                    return BadRequest(new { mensagem = $"Produto #{itemDto.ProdutoId} não encontrado" });
                }

                if (!produto.Disponivel)
                {
                    return BadRequest(new { mensagem = $"Produto #{produto.Id} está indisponível" });
                }

                pedido.Itens.Add(new ItemPedido
                {
                    PedidoId = pedido.Id,
                    ProdutoId = produto.Id,
                    Quantidade = itemDto.Quantidade,
                    PrecoUnitario = produto.Preco
                });
            }

            TheHomeDatabase.Pedidos.Add(pedido);

            return Created($"/pedidos/{pedido.Id}", PedidoResponse(pedido));
        }

        [HttpPut("{id}/status")]
        public ActionResult UpdateStatus(Guid id, [FromBody] PedidoStatusDto statusDto)
        {
            var pedido = TheHomeDatabase.Pedidos.FirstOrDefault(p => p.Id == id);

            if (pedido is null)
            {
                return NotFound(new { mensagem = $"Pedido #{id} não encontrado" });
            }

            pedido.Status = statusDto.Status;

            return Ok(PedidoResponse(pedido));
        }

        private static object PedidoResponse(Pedido pedido)
        {
            var usuario = TheHomeDatabase.Usuarios.FirstOrDefault(u => u.Id == pedido.UsuarioId);
            var itens = pedido.Itens.Select(item =>
            {
                var produto = TheHomeDatabase.Produtos.FirstOrDefault(p => p.Id == item.ProdutoId);

                return new
                {
                    item.Id,
                    item.ProdutoId,
                    Produto = produto?.Nome,
                    item.Quantidade,
                    item.PrecoUnitario,
                    Subtotal = item.Quantidade * item.PrecoUnitario
                };
            }).ToList();

            return new
            {
                pedido.Id,
                pedido.Data,
                pedido.Status,
                Usuario = usuario is null ? null : new { usuario.Id, usuario.Nome, usuario.Email },
                Itens = itens,
                Total = itens.Sum(i => i.Subtotal)
            };
        }
    }
}
