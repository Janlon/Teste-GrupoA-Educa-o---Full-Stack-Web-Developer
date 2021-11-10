using Teste.Domain.Entities;
using Teste.Domain.Interfaces.Repositories;
using Teste.Domain.Interfaces.Services;

namespace Teste.Domain.Services
{
    public class AlunoService : BaseService<Aluno>, IAlunoService
    {
        public AlunoService(IAlunoRepositorie repositorie) : base(repositorie)
        {

        }
    }
}
