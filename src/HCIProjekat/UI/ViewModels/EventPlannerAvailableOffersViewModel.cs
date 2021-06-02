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
        private readonly ITaskOfferService _taskOfferService;

        private int _taskId;
        public int TaskId
        {
            get { return _taskId; }
            set { _taskId = value; }
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

        public EventPlannerAvailableOffersViewModel(IApplicationContext context, ITaskOfferService taskOfferService) : base(context)
        {
            _taskOfferService = taskOfferService;

            Search = new DelegateCommand(() => UpdatePage(0));

            SearchQuery = string.Empty;
            Rows = 1;

            UpdatePage(0);
        }


        public override void UpdatePage(int pageNumber)
        {
            TaskOfferModels.Clear();
            var page = _taskOfferService.GetOffersForTask(_taskId, new OffersForTaskPageRequest { Page = pageNumber, Size = Size, SearchQuery = SearchQuery });
            foreach (var entity in page.Entities)
            {
                var offer = entity.Offer;
                TaskOfferModels.Add(new EventPlannerTaskOfferCardModel { Id = offer.Id, Name = offer.Name, Description = offer.Description, Image = ImageUtil.ConvertToImage(offer.Image) });
            }
            OnPageFetched(page);
        }
    }
}
