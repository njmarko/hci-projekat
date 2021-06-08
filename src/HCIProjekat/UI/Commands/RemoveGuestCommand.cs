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
    public class RemoveGuestCommand : ICommand
    {
        private readonly ClientSeatingLayoutViewModel _vm;
        private readonly IRequestService _requestService;
        private readonly int _requestId;
        private readonly int _guestId;

        public event EventHandler CanExecuteChanged;

        public RemoveGuestCommand(ClientSeatingLayoutViewModel vm, IRequestService requestService, int requestId, int guestId)
        {
            _vm = vm;
            _requestService = requestService;
            _requestId = requestId;
            _guestId = guestId;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var guest = _vm.Guests.SingleOrDefault(g => g.Id == _guestId);
            _requestService.RemoveGuest(_requestId, _guestId);
            _vm.Guests.Remove(guest);
            _vm.GuestCount--;
        }
    }
}
