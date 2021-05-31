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

    public class AdminEventPlannersViewModel : PagingViewModelBase
    {
        private readonly IEventPlannersService _eventPlannersService;

        private readonly IModalService _modalService;

        private string _searchQuery;
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value;
                OnPropertyChanged(nameof(SearchQuery));
            }
        }

        public ICommand SearchCommand { get; set; }
        public ObservableCollection<AdminEventPlannerCardModel> EventPlannerModels { get; private set; } = new ObservableCollection<AdminEventPlannerCardModel>();

        public AdminEventPlannersViewModel(IApplicationContext context, IEventPlannersService eventPlannersService, IModalService modalService) : base(context)
        {
            _eventPlannersService = eventPlannersService;
            SearchQuery = string.Empty;
            SearchCommand = new DelegateCommand(() => UpdatePage(0));
            _modalService = modalService;
            Columns = 4;
            UpdatePage(0);
        }

        public override void UpdatePage(int pageNumber)
        {
            EventPlannerModels.Clear();
            var page = _eventPlannersService.GetEventPlanners(new EventPlannersPage { Page = pageNumber, Size = Size, SearchQuery = SearchQuery });
            foreach (var entity in page.Entities)
            {

                var eventPlannerModel = new AdminEventPlannerCardModel
                {
                    Name = entity.FirstName + " " + entity.LastName,
                    Username = entity.Username,
                    DateOfBirth = entity.DateOfBirth.ToShortDateString(),
                    ActiveRequests = entity.AcceptedRequests.Count,
                    CompletedRequests = 150
                };
                eventPlannerModel.Delete = new DeleteEventPlannerCommand(this, entity.Id, entity.Username, _modalService, _eventPlannersService);
                EventPlannerModels.Add(eventPlannerModel);
            }
            OnPageFetched(page);
        }
    }
}
