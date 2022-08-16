using AgendaDeContatos.Core;
using AgendaDeContatos.Core.Interfaces;
using AgendaDeContatos.Infra;
using AgendaDeContatos.Infra.Interfaces;
using AgendaDeContatos.Infra.Models;
using AgendaDeContatos.Infra.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AgendaDeContatos
{
    internal class Program
    {
        private static IServiceCollection Services;
        public static IServiceProvider ServiceProvider;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            IConfiguration configuration = SetConfigurations();
            CreateServices(configuration);

            ApplicationConfiguration.Initialize();
            Application.Run(new FrmPrincipal());
        }

        static IConfiguration SetConfigurations()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddUserSecrets<Program>()
                .Build();
        }

        static void CreateServices(IConfiguration configuration)
        {
            Services = new ServiceCollection();
            Services.AddSingleton(configuration);            
            Services.AddScoped<IConnectionFactory, ConnectionFactory>();
            Services.AddScoped<IRepository<Categoria, int>, CategoriaRepository>();
            Services.AddScoped<IRepository<Contato, int>, ContatoRepository>();

            var optionsBuilder = new DbContextOptionsBuilder<AgendaDeContatosContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("SqlServer"));
            Services.AddSingleton(new AgendaDeContatosContext(optionsBuilder.Options));

            ServiceProvider = Services.BuildServiceProvider();
        }
    }
}