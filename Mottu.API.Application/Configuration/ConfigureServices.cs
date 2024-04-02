using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mottu.Service.Data;
using Microsoft.EntityFrameworkCore;
using Mottu.Service.Services;
using Mottu.Service.Interfaces;

namespace Mottu.API.Application.Configuration
{
    public static class ConfigureServices
    {
        public static string connectionStringSqlData;
        public static string databaseSqlData;

        public static IServiceCollection AddInfraServices(this IServiceCollection services, IConfiguration configuration)
        {
            connectionStringSqlData = $"Host=localhost;Port=5432;Username=postgres;Password=1234;Database=mottu;";

            services.AddDbContext<ApplicationContext>(options =>
                options.UseNpgsql(connectionStringSqlData,
                    builder => builder.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));

            services.AddTransient<IMotosService, MotosService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IEntregadorService, EntregadorService>();
            services.AddTransient<IPedidoService, PedidoService>();
            services.AddTransient<IAluguelService, AluguelService>();

            services.AddScoped<IApplicationContext>(provider => provider.GetRequiredService<ApplicationContext>());

            return services;
        }
    }
}
