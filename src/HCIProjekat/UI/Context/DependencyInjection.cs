using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Context.Locators;
using UI.Context.Routers;
using UI.Context.Stores;

namespace UI.Context
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationContext(this IServiceCollection services)
        {
            services.AddSingleton<IViewModelLocator, ViewModelLocator>();
            services.AddSingleton<IRouter, Router>();
            services.AddSingleton<IStore, Store>();
            services.AddSingleton<IApplicationContext, ApplicationContext>();
            return services;
        }
    }
}
