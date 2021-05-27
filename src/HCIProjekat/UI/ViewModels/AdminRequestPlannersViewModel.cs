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

namespace UI.ViewModels
{

    public class AdminRequestPlannerCardModel
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string DateOfBirth { get; set; }
        public int ActiveRequests { get; set; }
        public int CompletedRequests { get; set; }

    }

    public class AdminRequestPlannersViewModel : PagingViewModelBase
    {
        private readonly IEventPlannersService _eventPlannersService;

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
        public ObservableCollection<AdminRequestPlannerCardModel> EventPlannerModels { get; private set; } = new ObservableCollection<AdminRequestPlannerCardModel>();

        public AdminRequestPlannersViewModel(IApplicationContext context, IEventPlannersService eventPlannersService) : base(context)
        {
            _eventPlannersService = eventPlannersService;
            SearchQuery = string.Empty;
            SearchCommand = new DelegateCommand(() => UpdatePage(0));
            Columns = 4;
            UpdatePage(0);
        }

        public override void UpdatePage(int pageNumber)
        {
            EventPlannerModels.Clear();
            var page = _eventPlannersService.GetEventPlanners(new EventPlannersPage { Page = pageNumber, Size = Size, SearchQuery = SearchQuery });
            foreach (var entity in page.Entities)
            {

                EventPlannerModels.Add(new AdminRequestPlannerCardModel { Name = entity.FirstName + " " + entity.LastName, Username = entity.Username, DateOfBirth = entity.DateOfBirth.ToString(), ActiveRequests = 12, CompletedRequests = 150});
            }
            OnPageFetched(page);
        }
    }
}
