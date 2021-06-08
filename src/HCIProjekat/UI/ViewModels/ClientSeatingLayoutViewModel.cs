using Domain.Entities;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.Commands;
using UI.Context;

namespace UI.ViewModels
{
    public class GuestModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ChairId { get; set; }

        public ICommand RemoveGuest { get; set; }
    }

    public class ClientSeatingLayoutViewModel : ViewModelBase
    {
        private readonly IRequestService _requestService;
        private readonly int _requestId;

        private readonly Request _request;

        private string _guestName = "";
        public string GuestName
        {
            get { return _guestName; }
            set { _guestName = value; OnPropertyChanged(nameof(GuestName)); OnPropertyChanged(nameof(CanAddGuest)); }
        }
        public int _guestCount = 0;
        public int GuestCount
        {
            get { return _guestCount; }
            set { _guestCount = value; OnPropertyChanged(nameof(GuestCount)); OnPropertyChanged(nameof(GuestNumberText)); OnPropertyChanged(nameof(CanAddGuest)); }
        }

        public string GuestNumberText => $"Added guests: {GuestCount} / {_request.GuestNumber}";
        public bool CanAddGuest => !string.IsNullOrEmpty(GuestName) && GuestCount != _request.GuestNumber;

        public SeatingLayout SeatingLayout { get; private set; }
        public ObservableCollection<GuestModel> Guests { get; private set; }

        public ICommand AddGuest { get; private set; }

        public ClientSeatingLayoutViewModel(IApplicationContext context, IRequestService requestService, int requestId) : base(context)
        {
            _requestService = requestService;
            _requestId = requestId;

            _request = _requestService.GetWithGuests(_requestId);
            SeatingLayout = _requestService.GetSeatingLayout(_requestId);

            Guests = new ObservableCollection<GuestModel>(_request.Guests.Select(g => Map(g)));
            AddGuest = new AddGuestCommand(this, _requestService, _requestId);
        }

        public GuestModel Map(Guest guest)
        {
            return new GuestModel
            {
                Id = guest.Id,
                Name = guest.Name,
                ChairId = guest.ChairId,
                RemoveGuest = new RemoveGuestCommand(this, _requestService, _requestId, guest.Id)
            };
        }
    }
}
