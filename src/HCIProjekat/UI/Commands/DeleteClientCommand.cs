﻿using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToastNotifications.Messages;
using UI.Services.Interfaces;
using UI.ViewModels;

namespace UI.Commands
{
    public class DeleteClientCommand : ICommand
    {
        private readonly AdminClientsViewModel _adminClientsViewModel;
        private readonly int _clientId;
        private readonly string _clientUsername;
        private readonly IModalService _modalService;
        private readonly IClientService _clientService;

        public event EventHandler CanExecuteChanged;

        public DeleteClientCommand(AdminClientsViewModel adminClientsVm, int clientId, string clientUsername, IModalService modalService, IClientService clientService)
        {
            _adminClientsViewModel = adminClientsVm;
            _clientId = clientId;
            _clientUsername = clientUsername;
            _modalService = modalService;
            _clientService = clientService;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (_modalService.ShowConfirmationDialog($"Are you sure you want to delete client {_clientUsername}?"))
            {
                // undo redo
                var client = _clientService.Get(_clientId);
                _adminClientsViewModel.AddItem(client);

                _clientService.Delete(_clientId);
                if (_adminClientsViewModel.PaginationViewModel.TotalElements - _adminClientsViewModel.PaginationViewModel.Page * _adminClientsViewModel.PaginationViewModel.PerPage <= 1 && _adminClientsViewModel.PaginationViewModel.Page > 0)
                {
                    _adminClientsViewModel.UpdatePage(_adminClientsViewModel.PaginationViewModel.Page - 1);
                }
                else if (_adminClientsViewModel.PaginationViewModel.TotalElements - _adminClientsViewModel.PaginationViewModel.Page * _adminClientsViewModel.PaginationViewModel.PerPage > 1)
                {
                    _adminClientsViewModel.UpdatePage(_adminClientsViewModel.PaginationViewModel.Page);
                }
                else
                {
                    _adminClientsViewModel.UpdatePage(0);
                }
                _adminClientsViewModel.Context.Notifier.ShowInformation($"Client {_clientUsername} has been deleted successfully.");
            }
        }
    }
}
