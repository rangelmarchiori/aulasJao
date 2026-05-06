using System.ComponentModel.DataAnnotations;

namespace ApiFinanceiro.Dtos
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Senha { get; set; }
    }
}
