using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Context;

namespace UI.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public IApplicationContext Context { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;
        
        public string HelpPage { get; set; }

        public ViewModelBase(IApplicationContext context)
        {
            Context = context;
            HelpPage = "";
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
