using Teste.Domain.Entities;
using Teste.Domain.Interfaces.Repositories;
using Teste.Infra.Data.Contexts;

namespace Teste.Infra.Data.Repositories
{
    public class AlunoRepositorie : BaseRepositorie<Aluno>, IAlunoRepositorie
    {
        public AlunoRepositorie(Context context) : base(context)
        {
        }
    }
}
