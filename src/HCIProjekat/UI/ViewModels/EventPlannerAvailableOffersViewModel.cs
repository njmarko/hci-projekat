using Domain.Pagination.Requests;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToastNotifications.Messages;
using UI.Commands;
using UI.Context;
using UI.Services.Interfaces;
using UI.Util;

namespace UI.ViewModels
{
    public class EventPlannerAvailableOffersViewModel : PagingViewModelBase
    {
        private readonly IOfferService _offerService;
        private readonly ITaskOfferService _taskOfferService;
        private readonly IModalService _modalService;

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

        public EventPlannerTaskOffersViewModel AddedVm { get; set; }
        public ICommand Search { get; private set; }
        public ObservableCollection<EventPlannerTaskOfferCardModel> TaskOfferModels { get; private set; } = new ObservableCollection<EventPlannerTaskOfferCardModel>();

        public EventPlannerAvailableOffersViewModel(IApplicationContext context, IOfferService offerService, ITaskOfferService taskOfferService, IModalService modalService) : base(context)
        {
            _offerService = offerService;
            _taskOfferService = taskOfferService;
            _modalService = modalService;

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
                TaskOfferModels.Add(new EventPlannerTaskOfferCardModel { Id = entity.Id, Name = entity.Name, Description = entity.Description, Image = ImageUtil.ConvertToImage(entity.Image), ButtonContent = "Add", ButtonAction = new DelegateCommand(() => AddOfferForTask(entity.Id)) });
            }
            OnPageFetched(page);
        }

        private void AddOfferForTask(int offerId)
        {
            var ok = _modalService.ShowConfirmationDialog("Are you sure you want to add this offer to task?");
            if (ok)
            {
                _taskOfferService.AddOfferForTask(_taskId, offerId);
                UpdatePage(0);
                Context.Notifier.ShowInformation("Offer successfully to the task.");
                AddedVm.UpdatePage(0);
            }
        }
    }
}
