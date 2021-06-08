using Domain.Enums;
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
    public class EventPlannerTaskOffersViewModel : PagingViewModelBase
    {
        private readonly ITaskOfferService _taskOfferService;
        private readonly IModalService _modalService;
        public EventPlannerTaskDetailsViewModel TaskVm { get; private set; }

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

        private bool _addedDrop;
        public bool AddedDrop
        {
            get => _addedDrop;
            set
            {
                _addedDrop = value;
                OnPropertyChanged(nameof(AddedDrop));
            }
        }

        private ServiceTypeModel _offerType;
        public ServiceTypeModel OfferTypeValue
        {
            get { return _offerType; }
            set { _offerType = value; OnPropertyChanged(nameof(OfferTypeValue)); }
        }

        public EventPlannerAvailableOffersViewModel AvailableVm { get; set; }
        public ICommand Search { get; private set; }
        public ObservableCollection<EventPlannerTaskOfferCardModel> TaskOfferModels { get; private set; } = new ObservableCollection<EventPlannerTaskOfferCardModel>();

        public EventPlannerTaskOffersViewModel(IApplicationContext context, ITaskOfferService taskOfferService, IModalService modalService, EventPlannerTaskDetailsViewModel taskVm) : base(context)
        {
            _taskOfferService = taskOfferService;
            _modalService = modalService;
            TaskVm = taskVm;

            Search = new DelegateCommand(() => UpdatePage(0));

            SearchQuery = string.Empty;
            Rows = 1;

            AddedDrop = false;
        }

        public override void UpdatePage(int pageNumber)
        {
            TaskOfferModels.Clear();
            var page = _taskOfferService.GetOffersForTask(_taskId, new OffersForTaskPageRequest { Page = pageNumber, Size = Size, SearchQuery = SearchQuery });
            foreach (var entity in page.Entities)
            {
                var offer = entity.Offer;
                TaskOfferModels.Add(new EventPlannerTaskOfferCardModel { Id = offer.Id, Name = offer.Name, Description = offer.Description, Image = ImageUtil.ConvertToImage(offer.Image), IconKind = "Delete", ToolTip = "Remove from taks's added offers", ButtonAction = new DelegateCommand(() => RemoveOfferFromTask(entity.Id)) });
            }
            OnPageFetched(page);
        }

        private void RemoveOfferFromTask(int offerId)
        {
            var ok = _modalService.ShowConfirmationDialog("Are you sure you want to remove this offer from the task?");
            if (ok)
            {
                var taskOffer = _taskOfferService.RemoveOfferFromTask(offerId);
                taskOffer.Active = true;
                TaskVm.AddItem(taskOffer);
                UpdatePage(0);
                Context.Notifier.ShowInformation("Offer succesfully removed from the task.");
                AvailableVm.UpdatePage(0);
            }
            else
            {
                TaskVm.TabSelectedIndex = 0;
            }
        }
    }
}
