using ApiFinanceiro.Data;
using ApiFinanceiro.Dtos;
using ApiFinanceiro.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiFinanceiro.Controllers
{
    [Route("/produtos")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        [HttpGet]
        public ActionResult FindAll()
        {
            return Ok(TheHomeDatabase.Produtos.Select(ProdutoResponse));
        }

        [HttpGet("/cardapio")]
        public ActionResult Cardapio()
        {
            var produtosDisponiveis = TheHomeDatabase.Produtos
                .Where(p => p.Disponivel)
                .Select(ProdutoResponse);

            return Ok(produtosDisponiveis);
        }

        [HttpGet("{id}")]
        public ActionResult FindById(Guid id)
        {
            var produto = TheHomeDatabase.Produtos.FirstOrDefault(p => p.Id == id);

            if (produto is null)
            {
                return NotFound(new { mensagem = $"Produto #{id} não encontrado" });
            }

            return Ok(ProdutoResponse(produto));
        }

        [HttpPost]
        public ActionResult Create([FromBody] ProdutoDto produtoDto)
        {
            var categoriaExiste = TheHomeDatabase.Categorias.Any(c => c.Id == produtoDto.CategoriaId);

            if (!categoriaExiste)
            {
                return BadRequest(new { mensagem = $"Categoria #{produtoDto.CategoriaId} não encontrada" });
            }

            var produto = new Produto
            {
                Nome = produtoDto.Nome,
                Descricao = produtoDto.Descricao,
                Preco = produtoDto.Preco,
                CategoriaId = produtoDto.CategoriaId,
                Disponivel = produtoDto.Disponivel
            };

            TheHomeDatabase.Produtos.Add(produto);

            return Created($"/produtos/{produto.Id}", ProdutoResponse(produto));
        }

        [HttpPut("{id}")]
        public ActionResult Update(Guid id, [FromBody] ProdutoDto produtoDto)
        {
            var produto = TheHomeDatabase.Produtos.FirstOrDefault(p => p.Id == id);

            if (produto is null)
            {
                return NotFound(new { mensagem = $"Produto #{id} não encontrado" });
            }

            var categoriaExiste = TheHomeDatabase.Categorias.Any(c => c.Id == produtoDto.CategoriaId);

            if (!categoriaExiste)
            {
                return BadRequest(new { mensagem = $"Categoria #{produtoDto.CategoriaId} não encontrada" });
            }

            produto.Nome = produtoDto.Nome;
            produto.Descricao = produtoDto.Descricao;
            produto.Preco = produtoDto.Preco;
            produto.CategoriaId = produtoDto.CategoriaId;
            produto.Disponivel = produtoDto.Disponivel;

            return Ok(ProdutoResponse(produto));
        }

        [HttpDelete("{id}")]
        public ActionResult Remove(Guid id)
        {
            var produto = TheHomeDatabase.Produtos.FirstOrDefault(p => p.Id == id);

            if (produto is null)
            {
                return NotFound(new { mensagem = $"Produto #{id} não encontrado" });
            }

            TheHomeDatabase.Produtos.Remove(produto);

            return NoContent();
        }

        private static object ProdutoResponse(Produto produto)
        {
            var categoria = TheHomeDatabase.Categorias.FirstOrDefault(c => c.Id == produto.CategoriaId);

            return new
            {
                produto.Id,
                produto.Nome,
                produto.Descricao,
                produto.Preco,
                produto.Disponivel,
                Categoria = categoria is null ? null : new { categoria.Id, categoria.Nome }
            };
        }
    }
}
