using Domain;
using Domain.Entities;
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
            }
            //ApplicationDbContextSeed.Seed(context);

            var admin = new Admin { FirstName = "admin", LastName = "admin", Username = "vidojegavrilovic", Password = "test123", DateOfBirth = DateTime.Now };
            var adminService = _host.Services.GetRequiredService<IAdminService>();
            admin = adminService.Create(admin);

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
