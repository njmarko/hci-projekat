using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Diagnostics;

namespace UI.Commands
{
    public class OpenLinkCommand : ICommand
    {
        private string _host;
        public string Host
        {
            get { return _host; }
            set
            {
                _host = value;
            }
        }
        public event EventHandler CanExecuteChanged;


        public OpenLinkCommand()
        {
            Host = "https://hci-help.herokuapp.com/#";
           //Host = "http://localhost:8080/#";
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is string stringParam)
            {
                Process.Start(new ProcessStartInfo()
                {
                    FileName = $"{Host}/{stringParam}",
                    UseShellExecute = true
                });
            }
        }
    }
}
