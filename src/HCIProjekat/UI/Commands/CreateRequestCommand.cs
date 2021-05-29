using Domain.Entities;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using ToastNotifications.Messages;
using UI.Context;
using UI.Context.Stores;
using UI.ViewModels;

namespace UI.Commands
{
    public class CreateRequestCommand : ICommand
    {
        private readonly CreateRequestViewModel _createRequestVm;
        private readonly IRequestService _requestService;
        private readonly IApplicationContext _context;

        public event EventHandler CanExecuteChanged;

        public CreateRequestCommand(CreateRequestViewModel createRequestVm, IRequestService requestService, IApplicationContext context)
        {
            _createRequestVm = createRequestVm;
            _requestService = requestService;
            _context = context;

            _createRequestVm.PropertyChanged += _createRequestVm_PropertyChanged;
        }

        private void _createRequestVm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CreateRequestViewModel.CanCreateRequest))
            {
                CanExecuteChanged?.Invoke(sender, e);
            }
        }

        public bool CanExecute(object parameter)
        {
            return _createRequestVm.CanCreateRequest;
        }

        public void Execute(object parameter)
        {
            var clientId = 1; // TODO: Zameni ovo sa Context.Store.CurrentUser.Id
            var request = new Request 
            {
                Name = _createRequestVm.Name, 
                Type = _createRequestVm.RequestTypeValue.Type.Value, 
                Budget = _createRequestVm.Budget,
                BudgetFlexible = _createRequestVm.BudgetFlexible,
                GuestNumber = _createRequestVm.GuestNumber,
                Theme = _createRequestVm.Theme,
                Date = _createRequestVm.RequestDate,
                Notes = _createRequestVm.Notes,
                Tasks = new List<Task>(), 
            };
            _requestService.Create(clientId, request);
            _context.Notifier.ShowSuccess($"Request '{request.Name}' has been created.");
            (parameter as Window).Close();
        }
    }
}
