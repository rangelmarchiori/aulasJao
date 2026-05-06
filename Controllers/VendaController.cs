using ApiFinanceiro.Data;
using Microsoft.AspNetCore.Mvc;

namespace ApiFinanceiro.Controllers
{
    [Route("/vendas")]
    [ApiController]
    public class VendaController : ControllerBase
    {
        [HttpGet]
        public ActionResult FindAll()
        {
            var vendas = TheHomeDatabase.Pedidos.Select(pedido =>
            {
                var pagamento = TheHomeDatabase.Pagamentos.FirstOrDefault(p => p.PedidoId == pedido.Id);
                var total = pedido.Itens.Sum(i => i.Quantidade * i.PrecoUnitario);

                return new
                {
                    PedidoId = pedido.Id,
                    pedido.Data,
                    StatusPedido = pedido.Status,
                    Total = total,
                    Pagamento = pagamento is null ? null : new
                    {
                        pagamento.Id,
                        pagamento.FormaPagamento,
                        pagamento.Status
                    }
                };
            });

            return Ok(vendas);
        }
    }
}
