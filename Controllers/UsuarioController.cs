using ApiFinanceiro.Data;
using ApiFinanceiro.Dtos;
using ApiFinanceiro.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiFinanceiro.Controllers
{
    [Route("/usuarios")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        [HttpGet]
        public ActionResult FindAll()
        {
            return Ok(TheHomeDatabase.Usuarios.Select(u => new
            {
                u.Id,
                u.Nome,
                u.Email,
                u.Tipo
            }));
        }

        [HttpPost]
        public ActionResult Create([FromBody] UsuarioDto usuarioDto)
        {
            var emailJaExiste = TheHomeDatabase.Usuarios.Any(u =>
                u.Email.Equals(usuarioDto.Email, StringComparison.OrdinalIgnoreCase));

            if (emailJaExiste)
            {
                return BadRequest(new { mensagem = "Email já cadastrado" });
            }

            var usuario = new Usuario
            {
                Nome = usuarioDto.Nome,
                Email = usuarioDto.Email,
                Senha = usuarioDto.Senha,
                Tipo = usuarioDto.Tipo
            };

            TheHomeDatabase.Usuarios.Add(usuario);

            return Created($"/usuarios/{usuario.Id}", new
            {
                usuario.Id,
                usuario.Nome,
                usuario.Email,
                usuario.Tipo
            });
        }

        [HttpGet("{id}")]
        public ActionResult FindById(Guid id)
        {
            var usuario = TheHomeDatabase.Usuarios.FirstOrDefault(u => u.Id == id);

            if (usuario is null)
            {
                return NotFound(new { mensagem = $"Usuário #{id} não encontrado" });
            }

            return Ok(new
            {
                usuario.Id,
                usuario.Nome,
                usuario.Email,
                usuario.Tipo
            });
        }
    }
}
