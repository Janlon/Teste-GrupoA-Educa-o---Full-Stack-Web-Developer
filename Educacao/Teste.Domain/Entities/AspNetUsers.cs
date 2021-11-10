using Microsoft.AspNetCore.Identity;

namespace Teste.Domain.Entities
{
    public class AspNetUsers : IdentityUser
    {
        public string AlunoId { get; set; }
        public virtual Aluno Aluno { get; set; }
    }
}
