using Domain.Entities;
using Domain.Exceptions;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.Context.Routers;
using UI.ViewModels;

namespace UI.Commands
{
    public class RegisterEventPlannerCommand : ICommand
    {
        private readonly RegisterEventPlannerModel _registerVm;
        private readonly IEventPlannersService _eventPlannerService;
        private readonly IRouter _router;

        public event EventHandler CanExecuteChanged;

        public RegisterEventPlannerCommand(RegisterEventPlannerModel registerVm, IEventPlannersService eventPlannerService, IRouter router)
        {
            _registerVm = registerVm;
            _eventPlannerService = eventPlannerService;
            _router = router;
            _registerVm.PropertyChanged += _registerVm_PropertyChanged;
        }

        private void _registerVm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(RegisterViewModel.CanRegister))
            {
                CanExecuteChanged?.Invoke(sender, e);
            }
        }

        public bool CanExecute(object parameter)
        {
            return _registerVm.CanRegister;
        }

        public void Execute(object parameter)
        {
            try
            { 
                var registeredClient = _eventPlannerService.Create(new EventPlanner { FirstName = _registerVm.FirstName, LastName = _registerVm.LastName, Password = _registerVm.Password, Username = _registerVm.Username, DateOfBirth = _registerVm.DateOfBirth });
            } 
            catch (UsernameAlreadyExistsException exception)
            {
                _registerVm.UsernameError.ErrorMessage = exception.Message;
            }
        }
    }
}
