﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.ViewModels
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddViewModels(this IServiceCollection services)
        {
            services.AddTransient<MainViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<ClientRequestsViewModel>();
            return services;
        }
    }
}
