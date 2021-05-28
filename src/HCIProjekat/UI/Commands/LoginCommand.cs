using Domain.Entities;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UI.Context;
using UI.Context.Stores;
using UI.ViewModels;

namespace UI.Commands
{
    public class LoginCommand : ICommand
    {
        private readonly LoginViewModel _loginVm;
        private readonly IAuthService _authService;
        private readonly IApplicationContext _context;

        public event EventHandler CanExecuteChanged;

        public LoginCommand(LoginViewModel loginVm, IAuthService authService, IApplicationContext context)
        {
            _loginVm = loginVm;
            _authService = authService;
            _context = context;
            _loginVm.PropertyChanged += _loginVm_PropertyChanged;
        }

        private void _loginVm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LoginViewModel.CanLogin))
            {
                CanExecuteChanged?.Invoke(sender, e);
            }
        }

        public bool CanExecute(object parameter)
        {
            return _loginVm.CanLogin;
        }

        public void Execute(object parameter)
        {
            User user = _authService.Login(_loginVm.Username, _loginVm.Password);
            if (user == null)
            {
                _loginVm.ErrorViewModel.ErrorMessage = "Invalid username and/or password.";
                return;
            }
            _context.Store.CurrentUser = user;
            // TODO: Redirect the user to the apporopriate home page
            switch (user)
            {
                case Client:
                    MessageBox.Show($"Welcome client, {user.FirstName} {user.LastName}.");
                    break;
                case EventPlanner:
                    MessageBox.Show($"Welcome event planner, {user.FirstName} {user.LastName}.");
                    break;
                case Admin:
                    MessageBox.Show($"Welcome admin, {user.FirstName} {user.LastName}.");
                    break;
            }
        }
    }
}
