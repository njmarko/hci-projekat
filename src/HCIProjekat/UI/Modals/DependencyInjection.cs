using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Modals
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddModalWindows(this IServiceCollection services)
        {
            services.AddTransient<OfferModal>();
            services.AddTransient<RequestModal>();
            services.AddTransient<ConfirmationModal>();
            services.AddTransient<UpdateProfileInfoModal>();
            services.AddTransient<ChangePasswordModal>();
            services.AddTransient<CreateTaskModal>();
            return services;
        }
    }
}
