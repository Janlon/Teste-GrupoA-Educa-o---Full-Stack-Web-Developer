using System.ComponentModel.DataAnnotations;

namespace Teste.Application.DTOs
{
    public class AlunoDTO : BaseDTO
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage ="Nome é obrigatório.")]
        [StringLength(300, ErrorMessage = "O {0} deve ter pelo menos {2} caracteres.", MinimumLength = 6)]
        public string Nome { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage ="Email é obrigatório.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email em formato inválido.")]
        [MaxLength(256, ErrorMessage = "O {0} deve ter no máximo de {1} caracteres.")]
        public string Email { get; set; }

        [Display(Name = "CPF")]
        [Required(ErrorMessage="CPF é obrigatório.")]
        [StringLength(14, ErrorMessage = "O {0} deve ter {2} caracteres.", MinimumLength = 14)]
       // [CustomValidation.CustomValidationCPF(ErrorMessage="CPF inválido")]
        public string CPF { get; set; }

        [Display(Name = "RA")]
        [Required(ErrorMessage ="RA é obrigatório.")]
        [StringLength(14, ErrorMessage = "O {0} deve ter pelo menos {2} caracteres.", MinimumLength = 1)]
        public string RA { get; set; }
    }
}
