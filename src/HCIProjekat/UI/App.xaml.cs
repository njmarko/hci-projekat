using Domain;
using Domain.Entities;
using Domain.Pagination.Requests;
using Domain.Persistence;
using Domain.Services.Interfaces;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using UI.Context;
using UI.Context.Locators;
using UI.Context.Routers;
using UI.Context.Stores;
using UI.Modals;
using UI.Services;
using UI.ViewModels;

namespace UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = CreateHostBuilder().Build();
        }

        public static IHostBuilder CreateHostBuilder(string[] args = null)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(config =>
                {
                    config.AddJsonFile("appsettings.json");
                    config.AddEnvironmentVariables();
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddDomain();
                    services.AddInfrastructure(context.Configuration);
                    services.AddApplicationContext();
                    services.AddViewModels();
                    services.AddServices();
                    services.AddModalWindows();
                });
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();
            var contextFactory = _host.Services.GetRequiredService<IApplicationDbContextFactory>();
            using (var context = (ApplicationDbContext) contextFactory.CreateDbContext())
            {
                if (context.Database.IsSqlite())
                {
                    context.Database.Migrate();
                }
                ApplicationDbContextSeed.Seed(context);
            }

            //var service = _host.Services.GetRequiredService<IAuthService>();
            //var user = service.Login("peraperic1", "test12345");
            //Console.WriteLine(user);

            Window window = new MainWindow(_host.Services.GetRequiredService<MainViewModel>());
            window.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();
            base.OnExit(e);
        }
    }
}
