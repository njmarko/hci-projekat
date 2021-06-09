using Domain.Entities;
using Domain.Exceptions;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToastNotifications.Messages;
using UI.Context;
using UI.Context.Routers;
using UI.ViewModels;

namespace UI.Commands
{
    public class RegisterEventPlannerCommand : ICommand
    {
        private readonly RegisterEventPlannerViewModel _registerVm;
        private readonly IEventPlannersService _eventPlannerService;
        private readonly IRouter _router;
        private readonly IApplicationContext _context;
        private readonly AdminEventPlannersViewModel _adminEventPlannersVm;

        public event EventHandler CanExecuteChanged;

        public RegisterEventPlannerCommand(RegisterEventPlannerViewModel registerVm, IEventPlannersService eventPlannerService, IRouter router, IApplicationContext context, AdminEventPlannersViewModel adminEventPlannersVm)
        {
            _registerVm = registerVm;
            _eventPlannerService = eventPlannerService;
            _router = router;
            _registerVm.PropertyChanged += _registerVm_PropertyChanged;
            _context = context;
            _adminEventPlannersVm = adminEventPlannersVm;
        }

        private void _registerVm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(RegisterEventPlannerViewModel.CanRegister))
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
                var registeredEventPlanner = _eventPlannerService.Create(new EventPlanner { FirstName = _registerVm.FirstName, LastName = _registerVm.LastName, Password = _registerVm.Password, Username = _registerVm.Username, DateOfBirth = _registerVm.DateOfBirth });
                // undo redo
                registeredEventPlanner.Active = false;
                _adminEventPlannersVm.AddItem(registeredEventPlanner);

                _context.Notifier.ShowInformation($"Event planner {registeredEventPlanner.FirstName} {registeredEventPlanner.LastName} sucessfuly added.");
                _adminEventPlannersVm.UpdatePage(0);
                _registerVm.ResetFields();
            }
            catch (UsernameAlreadyExistsException exception)
            {
                _registerVm.UsernameError.ErrorMessage = exception.Message;
            }
        }
    }
}
