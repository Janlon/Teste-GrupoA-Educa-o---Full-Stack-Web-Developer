using AutoMapper;
using Teste.Application.DTOs;
using Teste.Application.Interfaces;
using Teste.Domain.Entities;
using Teste.Domain.Interfaces.Services;

namespace Teste.Application.Services
{
    public class AlunoApp : BaseApp<Aluno, AlunoDTO>, IAlunoApp
    {
        public AlunoApp(IMapper iMapper, IAlunoService service) : base(iMapper, service)
        {

        }
    }
}
