using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Identity;
using NetDevPack.Identity.Jwt;

namespace Teste.Infra.Ioc.Identity
{
    public static class ApiIdentityConfig
    {
        public static void AddApiIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            // Default EF Context for Identity (inside of the NetDevPack.Identity)
            services.AddIdentityEntityFrameworkContextConfiguration(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("conexaoSql"),
                        b => b.MigrationsAssembly("Teste.Infra.Ioc.Identity"));
                options.UseMySQL(configuration.GetConnectionString("conexaoMySql"),
                        b => b.MigrationsAssembly("Teste.Infra.Ioc.Identity"));
                options.EnableDetailedErrors();
            });

            // Default Identity configuration from NetDevPack.Identity
            services.AddIdentityConfiguration();

            // Default JWT configuration from NetDevPack.Identity
            services.AddJwtConfiguration(configuration, "AppSettings");
        }
    }
}
