using ApiFinanceiro.Data;
using ApiFinanceiro.Dtos;
using ApiFinanceiro.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiFinanceiro.Controllers
{
    [Route("/categorias")]
    [ApiController]
    public class CategoriaProdutoController : ControllerBase
    {
        [HttpGet]
        public ActionResult FindAll()
        {
            return Ok(TheHomeDatabase.Categorias);
        }

        [HttpPost]
        public ActionResult Create([FromBody] CategoriaProdutoDto categoriaDto)
        {
            var categoria = new CategoriaProduto { Nome = categoriaDto.Nome };
            TheHomeDatabase.Categorias.Add(categoria);

            return Created($"/categorias/{categoria.Id}", categoria);
        }

        [HttpPut("{id}")]
        public ActionResult Update(Guid id, [FromBody] CategoriaProdutoDto categoriaDto)
        {
            var categoria = TheHomeDatabase.Categorias.FirstOrDefault(c => c.Id == id);

            if (categoria is null)
            {
                return NotFound(new { mensagem = $"Categoria #{id} não encontrada" });
            }

            categoria.Nome = categoriaDto.Nome;

            return Ok(categoria);
        }

        [HttpDelete("{id}")]
        public ActionResult Remove(Guid id)
        {
            var categoria = TheHomeDatabase.Categorias.FirstOrDefault(c => c.Id == id);

            if (categoria is null)
            {
                return NotFound(new { mensagem = $"Categoria #{id} não encontrada" });
            }

            var categoriaEmUso = TheHomeDatabase.Produtos.Any(p => p.CategoriaId == id);

            if (categoriaEmUso)
            {
                return BadRequest(new { mensagem = "Categoria não pode ser removida porque possui produtos" });
            }

            TheHomeDatabase.Categorias.Remove(categoria);

            return NoContent();
        }
    }
}
