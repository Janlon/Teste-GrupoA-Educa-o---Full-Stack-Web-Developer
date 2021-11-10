using System.ComponentModel.DataAnnotations;

namespace Teste.Application.DTOs
{
    public class AlunoDTO : BaseDTO
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string CPF { get; set; }
    }
}
