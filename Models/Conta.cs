namespace ApiFinanceiro.Models
{
    public class Conta
    {
        public Guid Id { get; set; } = Guid.NewGuid();

	public required string Descricao { get; set; }
	
        public required string Categoria { get; set; }

        public decimal Valor { get; set; }

        public DateOnly DataPrevisao { get; set; }

        public DateOnly? DataRecebimento { get; set; }

        public string  Situacao { get; set; } ;

        public string? Observacao { get; set; }

    }
}
