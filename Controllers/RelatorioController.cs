using ApiFinanceiro.Data;
using Microsoft.AspNetCore.Mvc;

namespace ApiFinanceiro.Controllers
{
    [Route("/relatorios")]
    [ApiController]
    public class RelatorioController : ControllerBase
    {
        [HttpGet("vendas")]
        public ActionResult RelatorioVendas()
        {
            var pedidos = TheHomeDatabase.Pedidos;
            var pagamentosAprovados = TheHomeDatabase.Pagamentos
                .Where(p => p.Status.Equals("Aprovado", StringComparison.OrdinalIgnoreCase)
                    || p.Status.Equals("Pago", StringComparison.OrdinalIgnoreCase))
                .ToList();

            var pedidoIdsPagos = pagamentosAprovados.Select(p => p.PedidoId).ToHashSet();
            var pedidosPagos = pedidos.Where(p => pedidoIdsPagos.Contains(p.Id)).ToList();

            return Ok(new
            {
                TotalPedidos = pedidos.Count,
                PedidosPagos = pedidosPagos.Count,
                PedidosPendentes = pedidos.Count - pedidosPagos.Count,
                TotalVendido = pedidosPagos.Sum(p => p.Itens.Sum(i => i.Quantidade * i.PrecoUnitario)),
                ProdutosVendidos = pedidosPagos
                    .SelectMany(p => p.Itens)
                    .GroupBy(i => i.ProdutoId)
                    .Select(g =>
                    {
                        var produto = TheHomeDatabase.Produtos.FirstOrDefault(p => p.Id == g.Key);

                        return new
                        {
                            ProdutoId = g.Key,
                            Produto = produto?.Nome,
                            Quantidade = g.Sum(i => i.Quantidade),
                            Total = g.Sum(i => i.Quantidade * i.PrecoUnitario)
                        };
                    })
            });
        }
    }
}
