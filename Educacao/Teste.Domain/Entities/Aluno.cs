using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Teste.Domain.Entities
{
    [Index(nameof(CPF), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    public class Aluno : BaseEntitie
    {
        private string _Nome;
        public string Nome
        {
            get => _Nome;
            set => _Nome = !string.IsNullOrEmpty(value) ? value.ToUpper() : value;
        }
        private string _Email;
        public string Email
        {
            get => _Email;
            set => _Email = !string.IsNullOrEmpty(value) ? value.ToLower() : value;
        }
        public string CPF { get; set; }
    }
}
