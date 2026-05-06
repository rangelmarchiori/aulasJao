namespace ApiFinanceiro.Models
{
    public class CategoriaProduto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Nome { get; set; }
    }
}
