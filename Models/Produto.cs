namespace ApiFinanceiro.Models
{
    public class Produto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Nome { get; set; }
        public required string Descricao { get; set; }
        public required decimal Preco { get; set; }
        public required Guid CategoriaId { get; set; }
        public bool Disponivel { get; set; } = true;
    }
}
