using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Context;

namespace UI.ViewModels
{
    public class DemoViewModel : ViewModelBase
    {
        private string _header;

        public string Header
        {
            get { return _header; }
            set 
            { 
                _header = value;
                OnPropertyChanged(nameof(Header));
            }
        }


        public DemoViewModel(IApplicationContext context) : base(context)
        {
            Header = "DEMO";
        }
    }
}
