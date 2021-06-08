using Domain.Entities;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.ViewModels;

namespace UI.Commands
{
    public class AddGuestCommand : ICommand
    {
        private readonly ClientSeatingLayoutViewModel _vm;
        private readonly IRequestService _requestService;
        private readonly int _requestId;

        public event EventHandler CanExecuteChanged;

        public AddGuestCommand(ClientSeatingLayoutViewModel vm, IRequestService requestService, int requestId)
        {
            _vm = vm;
            _requestService = requestService;
            _requestId = requestId;

            _vm.PropertyChanged += _vm_PropertyChanged;
        }

        private void _vm_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ClientSeatingLayoutViewModel.CanAddGuest))
            {
                CanExecuteChanged?.Invoke(sender, e);
            }
        }

        public bool CanExecute(object parameter)
        {
            return _vm.CanAddGuest;
        }

        public void Execute(object parameter)
        {
            var guest = new Guest { Name = _vm.GuestName };
            _requestService.AddGuest(guest, _requestId);
            _vm.Guests.Add(_vm.Map(guest));
            _vm.GuestCount++;
            _vm.GuestName = "";
        }
    }
}
