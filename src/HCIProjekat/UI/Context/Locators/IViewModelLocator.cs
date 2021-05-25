using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.ViewModels;

namespace UI.Context.Locators
{
    public interface IViewModelLocator
    {
        public ViewModelBase Get<T>() where T : ViewModelBase;
    }
}
