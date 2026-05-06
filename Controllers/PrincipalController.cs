using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiFinanceiro.Controllers
{
    [Route("/")]
    [ApiController]
    public class PrincipalController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(new
            {
                api = "The Home API",
                status = "up",
                recursos = new[]
                {
                    "/auth/login",
                    "/usuarios",
                    "/cardapio",
                    "/categorias",
                    "/produtos",
                    "/pedidos",
                    "/pagamentos",
                    "/vendas",
                    "/relatorios/vendas"
                }
            });
        }
    }
}
