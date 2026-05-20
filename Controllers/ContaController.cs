using ApiFinanceiro.Dtos;
using ApiFinanceiro.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiFinanceiro.Controllers
{
    [Route("/conta")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private static List<Conta> listaContas = new()
        {
            new Conta {
                Descricao = "Fulano",
                Valor = 500,
                Categoria = "Divida",
                DataPrevisao = new DateOnly(2026, 08, 15),
                Observacao = "Aberto",
                Situacao = "Aberto",
                DataRecebimento = new DateOnly(2026, 08, 15)
            }
        };

        [HttpGet()]
        public ActionResult FindAll()
        {
            return Ok(listaContas);
        }

        [HttpPost()]
        public ActionResult Create([FromBody] ContaDto novaConta)
        {
            var conta = new Conta
            {
                Descricao = novaConta.Descricao,
                Valor = novaConta.Valor,
                Categoria = novaConta.Categoria,
                DataPrevisao = novaConta.DataPrevisao,
                Observacao = novaConta.Observacao,
                Situacao = novaConta.Situacao,
                DataRecebimento = new DateOnly(0001, 01, 01)
            };

            listaContas.Add(conta);

            return Created("", conta);
        }

        [HttpGet("{id}")]
        public ActionResult FindById(Guid id)
        {
            var conta = listaContas.FirstOrDefault(c => c.Id == id);

            if (conta is null)
            {
                return NotFound(new { mensagem = $"Conta #{id} não encontrada" });
            }

            return Ok(conta);
        }

        [HttpPut("{id}")]
        public ActionResult Update(Guid id, [FromBody] ContaUpdateDto contaDto)
        {
            var conta = listaContas.FirstOrDefault(c => c.Id == id);

            if (conta is null)
            {
                return NotFound(new { mensagem = $"Conta #{id} não encontrada" });
            }

            conta.Descricao = contaDto.Descricao;
            conta.Valor = contaDto.Valor;
            conta.DataPrevisao = contaDto.DataPrevisao;
            conta.Categoria = contaDto.Categoria;
            conta.Observacao = contaDto.Observacao;
            conta.Situacao = contaDto.Situacao;
            conta.DataRecebimento = contaDto.DataRecebimento;

            return Ok(conta);
        }

        [HttpDelete("{id}")]
        public ActionResult Remove(Guid id)
        {
            var conta = listaContas.FirstOrDefault(c => c.Id == id);

            if (conta is null)
            {
                return NotFound(new { mensagem = $"Conta #{id} não encontrada" });
            }

            listaContas.Remove(conta);

            return NoContent();
        }
    }
}
