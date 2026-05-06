using ApiFinanceiro.Models;

namespace ApiFinanceiro.Data
{
    public static class TheHomeDatabase
    {
        public static List<Usuario> Usuarios { get; } = new()
        {
            new Usuario
            {
                Nome = "Administrador",
                Email = "admin@thehome.com",
                Senha = "admin123",
                Tipo = "admin"
            },
            new Usuario
            {
                Nome = "Cliente Exemplo",
                Email = "cliente@thehome.com",
                Senha = "cliente123",
                Tipo = "cliente"
            }
        };

        public static List<CategoriaProduto> Categorias { get; } = new()
        {
            new CategoriaProduto { Nome = "Lanches" },
            new CategoriaProduto { Nome = "Bebidas" }
        };

        public static List<Produto> Produtos { get; } = new()
        {
            new Produto
            {
                Nome = "Burger Black",
                Descricao = "Hamburguer artesanal da casa",
                Preco = 28.90m,
                CategoriaId = Categorias[0].Id,
                Disponivel = true
            },
            new Produto
            {
                Nome = "Refrigerante lata",
                Descricao = "Bebida gelada 350ml",
                Preco = 6.00m,
                CategoriaId = Categorias[1].Id,
                Disponivel = true
            }
        };

        public static List<Pedido> Pedidos { get; } = new();
        public static List<Pagamento> Pagamentos { get; } = new();
    }
}
