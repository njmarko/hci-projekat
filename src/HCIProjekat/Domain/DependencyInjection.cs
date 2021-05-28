﻿using Domain.Services;
using Domain.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddSingleton<IAdminService, AdminService>();
            services.AddSingleton<IClientService, ClientService>();
            services.AddSingleton<IEventPlannersService, EventPlannersService>();
            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<IPartnersService, PartnersService>();
            return services;
        }
    }
}
