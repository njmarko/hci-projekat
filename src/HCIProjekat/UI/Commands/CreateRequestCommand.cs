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
        private readonly ClientRequestsViewModel _clientRequestsVm;
        private readonly IRequestService _requestService;
        private readonly IApplicationContext _context;
        private readonly int _requestId;

        public event EventHandler CanExecuteChanged;

        public CreateRequestCommand(CreateRequestViewModel createRequestVm, ClientRequestsViewModel clientRequestsVm, IRequestService requestService, IApplicationContext context, int requestId)
        {
            _createRequestVm = createRequestVm;
            _clientRequestsVm = clientRequestsVm;
            _requestService = requestService;
            _context = context;
            _requestId = requestId;

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
            var window = parameter as Window;
            if (_requestId == -1)
            {
                Create(window);
            }
            else
            {
                Update(window);
            }
        }

        private void Create(Window window)
        {
            var clientId = _context.Store.CurrentUser.Id;
            var request = new Request
            {
                Name = _createRequestVm.Name,
                Type = _createRequestVm.RequestTypeValue.Type.Value,
                Budget = int.Parse(_createRequestVm.Budget),
                BudgetFlexible = _createRequestVm.BudgetFlexible,
                GuestNumber = int.Parse(_createRequestVm.GuestNumber),
                Theme = _createRequestVm.Theme,
                Date = _createRequestVm.RequestDate,
                Notes = _createRequestVm.Notes,
                Tasks = new List<Task>(),
            };
            var created = _requestService.Create(clientId, request);
            created.Active = false;
            _clientRequestsVm.AddItem(created);
            _context.Notifier.ShowInformation($"Request '{request.Name}' has been created.");
            window.DialogResult = true;
            window.Close();
        }

        private void Update(Window window)
        {
            var request = _requestService.Get(_requestId);
            _clientRequestsVm.AddItem(request);
            request.Name = _createRequestVm.Name;
            request.Type = _createRequestVm.RequestTypeValue.Type.Value;
            request.Budget = int.Parse(_createRequestVm.Budget);
            request.BudgetFlexible = _createRequestVm.BudgetFlexible;
            request.GuestNumber = int.Parse(_createRequestVm.GuestNumber);
            request.Theme = _createRequestVm.Theme;
            request.Date = _createRequestVm.RequestDate;
            request.Notes = _createRequestVm.Notes;
            _requestService.Update(request);
            _context.Notifier.ShowInformation($"Request '{request.Name}' has been updated.");
            window.DialogResult = true;
            window.Close();
        }
    }
}
