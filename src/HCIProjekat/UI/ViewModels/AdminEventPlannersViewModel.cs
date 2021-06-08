using Domain.Entities;
using Domain.Pagination.Requests;
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
using UI.Modals;
using UI.Services.Interfaces;

namespace UI.ViewModels
{

    public class AdminEventPlannerCardModel
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string DateOfBirth { get; set; }
        public int ActiveRequests { get; set; }
        public int CompletedRequests { get; set; }

        public ICommand Delete { get; set; }

    }

    public class AdminEventPlannersViewModel : UndoModelBase<EventPlanner>
    {
        private readonly IEventPlannersService _eventPlannersService;

        private readonly IModalService _modalService;

        private readonly DateTime _bornBeforeInitial = DateTime.Now.AddDays(1);
        private readonly DateTime _bornAfterInitial = DateTime.Now.AddYears(-100);

        private string _query = string.Empty;
        public string Query
        {
            get => _query;
            set
            {
                _query = value;
                OnPropertyChanged(nameof(Query));
            }
        }

        private DateTime _bornBefore;
        public DateTime BornBefore
        {
            get { return _bornBefore; }
            set { _bornBefore = value; OnPropertyChanged(nameof(BornBefore)); }
        }

        private DateTime _bornAfter;
        public DateTime BornAfter
        {
            get { return _bornAfter; }
            set { _bornAfter = value; OnPropertyChanged(nameof(BornAfter)); }
        }

        private bool _hasActiveRequests = false;
        public bool HasActiveRequests
        {
            get => _hasActiveRequests;
            set
            {
                _hasActiveRequests = value;
                OnPropertyChanged(nameof(HasActiveRequests));
            }
        }

        public ICommand Search { get; set; }

        public ICommand AddEventPlanner { get; private set; }

        public ICommand Clear { get; private set; }


        public ObservableCollection<AdminEventPlannerCardModel> EventPlannerModels { get; private set; } = new ObservableCollection<AdminEventPlannerCardModel>();

        public AdminEventPlannersViewModel(IApplicationContext context, IEventPlannersService eventPlannersService, IModalService modalService) : base(context, eventPlannersService, modalService)
        {
            _eventPlannersService = eventPlannersService;
            Query = string.Empty;
            Search = new DelegateCommand(() => UpdatePage(0));
            Clear = new DelegateCommand(ClearFilters);
            _bornBefore = _bornBeforeInitial;
            _bornAfter = _bornAfterInitial;
            AddEventPlanner = new DelegateCommand(OpenRegisterEventPlannerModal);
            _modalService = modalService;
            Columns = 4;
            UpdatePage(0);
            HelpPage = "admin-event-planners";
        }

        private void OpenRegisterEventPlannerModal()
        {
            _modalService.ShowModal<AddEventPlannerModal>(new RegisterEventPlannerViewModel(Context, _eventPlannersService, this));
        }

        public void ClearFilters()
        {
            BornAfter = _bornAfterInitial;
            BornBefore = _bornBeforeInitial;
            Query = string.Empty;
            HasActiveRequests = false;
            UpdatePage(0);
        }

        public override void UpdatePage(int pageNumber)
        {
            EventPlannerModels.Clear();
            var page = _eventPlannersService.GetEventPlanners(new EventPlannersPage { Page = pageNumber, Size = Size, Query = Query, BornAfter = BornAfter, BornBefore = BornBefore, HasActiveRequests = HasActiveRequests });
            foreach (var entity in page.Entities)
            {
                var eventPlannerModel = new AdminEventPlannerCardModel
                {
                    Name = entity.FirstName + " " + entity.LastName,
                    Username = entity.Username,
                    DateOfBirth = entity.DateOfBirth.ToShortDateString(),
                    ActiveRequests = entity.AcceptedRequests.Count(r => r.Date < DateTime.Now),
                    CompletedRequests = entity.AcceptedRequests.Count(r => r.Date >= DateTime.Now),
                };
                eventPlannerModel.Delete = new DeleteEventPlannerCommand(this, entity.Id, entity.Username, _modalService, _eventPlannersService);
                EventPlannerModels.Add(eventPlannerModel);
            }
            OnPageFetched(page);
        }
    }
}
