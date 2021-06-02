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
using UI.Util;

namespace UI.ViewModels
{
    public class EventPlannerAvailableOffersViewModel : PagingViewModelBase
    {
        private readonly IOfferService _offerService;

        private int _taskId;
        public int TaskId
        {
            get { return _taskId; }
            set { _taskId = value; UpdatePage(0); }
        }

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

        public ICommand Search { get; private set; }
        public ObservableCollection<EventPlannerTaskOfferCardModel> TaskOfferModels { get; private set; } = new ObservableCollection<EventPlannerTaskOfferCardModel>();

        public EventPlannerAvailableOffersViewModel(IApplicationContext context, IOfferService offerService) : base(context)
        {
            _offerService = offerService;

            Search = new DelegateCommand(() => UpdatePage(0));

            SearchQuery = string.Empty;
            Rows = 1;
        }


        public override void UpdatePage(int pageNumber)
        {
            TaskOfferModels.Clear();
            var page = _offerService.GetAvailableOffersForTask(_taskId, new OffersForTaskPageRequest { Page = pageNumber, Size = Size, SearchQuery = SearchQuery });
            foreach (var entity in page.Entities)
            {
                TaskOfferModels.Add(new EventPlannerTaskOfferCardModel { Id = entity.Id, Name = entity.Name, Description = entity.Description, Image = ImageUtil.ConvertToImage(entity.Image) });
            }
            OnPageFetched(page);
        }
    }
}
