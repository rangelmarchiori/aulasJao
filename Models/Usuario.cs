namespace ApiFinanceiro.Models
{
    public class Usuario
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
        public required string Tipo { get; set; }
    }
}
