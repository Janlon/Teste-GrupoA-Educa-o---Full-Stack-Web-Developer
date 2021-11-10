using Microsoft.Extensions.Logging;
using Teste.Application.DTOs;
using Teste.Application.Interfaces;
using Teste.Domain.Entities;

namespace Teste.Services.Api.Controllers
{
    public class AlunoController : BaseController<Aluno, AlunoDTO>
    {
        public AlunoController(IAlunoApp app, ILogger<AlunoController> logger) : base(app, logger)
        {
        }
    }
}
