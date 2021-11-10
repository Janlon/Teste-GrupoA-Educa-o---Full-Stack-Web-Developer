using Microsoft.Extensions.DependencyInjection;
using Teste.Application.Interfaces;
using Teste.Application.Services;
using Teste.Domain.Interfaces.Repositories;
using Teste.Domain.Interfaces.Services;
using Teste.Domain.Services;
using Teste.Infra.Data.Repositories;

namespace Teste.Infra.Ioc
{
    public static class DependencyInjector
    {
        public static void Register(IServiceCollection svcCollection)
        {
            #region Application
            svcCollection.AddScoped(typeof(IBaseApp<,>), typeof(BaseApp<,>));
            //Aluno
            svcCollection.AddScoped<IAlunoApp, AlunoApp>();        
            #endregion

            #region Domain
            svcCollection.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            //Aluno
            svcCollection.AddScoped<IAlunoService, AlunoService>();
            #endregion

            #region Repositorie
            svcCollection.AddScoped(typeof(IBaseRepositorie<>), typeof(BaseRepositorie<>));
            //Aluno
            svcCollection.AddScoped<IAlunoRepositorie, AlunoRepositorie>();
            #endregion
        }
    }
}
