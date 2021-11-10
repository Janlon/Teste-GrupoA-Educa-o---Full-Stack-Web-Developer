using Teste.Application.DTOs;
using Teste.Domain.Entities;

namespace Teste.Application.Interfaces
{
    public interface IAlunoApp : IBaseApp<Aluno, AlunoDTO>
    {
    }
}
