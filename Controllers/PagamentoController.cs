using ApiFinanceiro.Data;
using ApiFinanceiro.Dtos;
using ApiFinanceiro.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiFinanceiro.Controllers
{
    [Route("/pagamentos")]
    [ApiController]
    public class PagamentoController : ControllerBase
    {
        [HttpGet]
        public ActionResult FindAll()
        {
            return Ok(TheHomeDatabase.Pagamentos);
        }

        [HttpGet("{id}")]
        public ActionResult FindById(Guid id)
        {
            var pagamento = TheHomeDatabase.Pagamentos.FirstOrDefault(p => p.Id == id);

            if (pagamento is null)
            {
                return NotFound(new { mensagem = $"Pagamento #{id} não encontrado" });
            }

            return Ok(pagamento);
        }

        [HttpPost]
        public ActionResult Create([FromBody] PagamentoDto pagamentoDto)
        {
            var pedidoExiste = TheHomeDatabase.Pedidos.Any(p => p.Id == pagamentoDto.PedidoId);

            if (!pedidoExiste)
            {
                return BadRequest(new { mensagem = $"Pedido #{pagamentoDto.PedidoId} não encontrado" });
            }

            var pedidoJaTemPagamento = TheHomeDatabase.Pagamentos.Any(p => p.PedidoId == pagamentoDto.PedidoId);

            if (pedidoJaTemPagamento)
            {
                return BadRequest(new { mensagem = "Pedido já possui pagamento cadastrado" });
            }

            var pagamento = new Pagamento
            {
                PedidoId = pagamentoDto.PedidoId,
                FormaPagamento = pagamentoDto.FormaPagamento,
                Status = pagamentoDto.Status
            };

            TheHomeDatabase.Pagamentos.Add(pagamento);

            return Created($"/pagamentos/{pagamento.Id}", pagamento);
        }

        [HttpPut("{id}/status")]
        public ActionResult UpdateStatus(Guid id, [FromBody] PagamentoStatusDto statusDto)
        {
            var pagamento = TheHomeDatabase.Pagamentos.FirstOrDefault(p => p.Id == id);

            if (pagamento is null)
            {
                return NotFound(new { mensagem = $"Pagamento #{id} não encontrado" });
            }

            pagamento.Status = statusDto.Status;

            return Ok(pagamento);
        }
    }
}
