using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.ViewModels;

namespace UI.Context.Locators
{
    public class ViewModelLocator : IViewModelLocator
    {
        private readonly IServiceProvider _serviceProvider;

        public ViewModelLocator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ViewModelBase Get<T>() where T : ViewModelBase
        {
            return _serviceProvider.GetRequiredService<T>();
        }
    }
}
