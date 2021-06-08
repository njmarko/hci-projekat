﻿using Domain.Entities;
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

        private string _guestSearch = "";
        public string GuestSearch
        {
            get { return _guestSearch; }
            set { _guestSearch = value; OnPropertyChanged(nameof(GuestSearch)); UpdateGuestSearch(); }
        }
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
        public ObservableCollection<GuestModel> Guests { get; private set; } = new ObservableCollection<GuestModel>();

        public ICommand AddGuest { get; private set; }

        public ClientSeatingLayoutViewModel(IApplicationContext context, IRequestService requestService, int requestId) : base(context)
        {
            _requestService = requestService;
            _requestId = requestId;

            _request = _requestService.GetWithGuests(_requestId);
            SeatingLayout = _requestService.GetSeatingLayout(_requestId) ?? CreateDebugSeatingLayout();

            AddGuest = new AddGuestCommand(this, _requestService, _requestId);
            UpdateGuestSearch();
        }

        private SeatingLayout CreateDebugSeatingLayout()
        {
            return new SeatingLayout
            {
                Tables = new List<Table>()
                {
                    new Table()
                    {
                        Id = 1,
                        Label = "Table #1",
                        X = 70,
                        Y = 70,
                        Radius = 40,
                        Chairs = new List<Chair>()
                        {
                            new Chair() {Id = 1, X = 10, Y = 10, Label = "1"},
                            new Chair() {Id = 2, X = 10, Y = 90, Label = "2"},
                            new Chair() {Id = 3, X = 180, Y = 10, Label = "3"},
                        }
                    }
                }
            };
        }

        public GuestModel DropGuest(int guestId, double x, double y)
        {
            var guest = _request.Guests.SingleOrDefault(g => g.Id == guestId);
            var chair = ClosestChair(x, y);
            guest.ChairId = chair.Id;
            _requestService.SetGuestChair(guestId, chair.Id);
            UpdateGuestSearch();
            return Map(guest);
        }

        public double Distance(ILayoutItem chair, double x, double y)
        {
            double xDiff = chair.X - x;
            double yDiff = chair.Y - y;
            return Math.Sqrt(xDiff * xDiff + yDiff * yDiff);
        }

        public Chair ClosestChair(double x, double y)
        {
            var chair = SeatingLayout.Tables.SelectMany(t => t.Chairs).Where(c =>
            {
                return _request.Guests.FirstOrDefault(g => g.ChairId == c.Id) == null;
            }).OrderBy(c => Distance(c, x, y)).FirstOrDefault();
            return chair;
        }

        public void UpdateGuestSearch()
        {
            Guests.Clear();
            foreach (var guest in _request.Guests.Where(g => g.Name.ToLower().Contains(GuestSearch.ToLower()) && g.ChairId == 0).Select(g => Map(g)))
            {
                Guests.Add(guest);
            }
        }

        public void InsertGuest(Guest guest)
        {
            _request.Guests.Add(guest);
        }

        public void RemoveGuest(int guestId)
        {
            _request.Guests.Remove(_request.Guests.SingleOrDefault(g => g.Id == guestId));
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
