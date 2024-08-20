using B1TestProject.Core.Interfaces;
using B1TestProject.Core.Services;
using B1TestProject.Domain.Entities;
using B1TestProject.Infrastructure;
using B1TestProject.Infrastructure.Interfaces;
using B1TestProject.Infrastructure.Repositories;
using B1TestProject.Interfaces;
using B1TestProject.Services;
using B1TestProject.Utilities;
using B1TestProject.ViewModels;
using B1TestProject.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Windows;

namespace B1TestProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IHost Host { get; private set; }
        private ITextFilesService _service;
        private ICancellationTokenService _token;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSingleton(x =>
                    {
                        var configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "appsettings.json");
                        var config = Newtonsoft.Json.JsonConvert.DeserializeObject<Config>(File.ReadAllText(configPath))
                            ?? throw new Exception("cannot read appsettings.json file");
                        return config;
                    });

                    services.AddDbContext<TestProjDbContext>((provider, x) =>
                    {
                        var config = provider.GetRequiredService<Config>();
                        x.UseNpgsql(config.ConnectionString);
                    }, ServiceLifetime.Transient);

                    services.AddTransient<MainWindow>();
                    services.AddTransient<NavigationVM>();
                    services.AddTransient<TextFilesTaskView>();
                    services.AddTransient<TextFilesTaskVM>();
                    services.AddTransient<ExcelFilesTaskView>();
                    services.AddTransient<ExcelFilesTaskVM>();

                    services.AddTransient<ITextFilesService, TextFilesService>();
                    services.AddTransient<IExcelFilesService, ExcelFilesService>();
                    services.AddTransient<ICancellationTokenService, CancellationTokenService>();

                    services.AddTransient<IDTOConverter, DTOConverter>();
                    services.AddTransient<ITextLinesService, TextLinesService>();
                    services.AddTransient<IBalanceSheetService, BalanceSheetService>();
                    services.AddTransient<IExcelDBService, ExcelDBService>();

                    services.AddTransient(typeof(ITextRepository<>), typeof(TextRepository<>));
                    services.AddTransient(typeof(IExcelEntryRepository<>), typeof(ExcelEntryRepository<>));
                    services.AddTransient(typeof(IExcelFileRepository<>), typeof(ExcelFileRepository<>));

                }).Build();

            var mainWIndow = Host.Services.GetRequiredService<MainWindow>();
            mainWIndow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            string directoryPath = "..\\GeneretedFiles";

            if (Directory.Exists(directoryPath))
            {
                Directory.Delete(directoryPath, true);
            }
        }
    }
}
