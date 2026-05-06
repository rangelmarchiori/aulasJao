using ApiFinanceiro.Data;
using ApiFinanceiro.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ApiFinanceiro.Controllers
{
    [Route("/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto loginDto)
        {
            var usuario = TheHomeDatabase.Usuarios.FirstOrDefault(u =>
                u.Email.Equals(loginDto.Email, StringComparison.OrdinalIgnoreCase)
                && u.Senha == loginDto.Senha);

            if (usuario is null)
            {
                return Unauthorized(new { mensagem = "Email ou senha inválidos" });
            }

            return Ok(new
            {
                usuario.Id,
                usuario.Nome,
                usuario.Email,
                usuario.Tipo,
                token = $"fake-token-{usuario.Id}"
            });
        }
    }
}
