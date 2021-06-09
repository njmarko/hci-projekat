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

        private bool _availableDrop;
        public bool AvailableDrop
        {
            get => _availableDrop;
            set
            {
                _availableDrop = value;
                OnPropertyChanged(nameof(AvailableDrop));
            }
        }

        public EventPlannerTaskOffersViewModel AddedVm { get; set; }
        public override ICommand Search { get; set; }
        public ObservableCollection<EventPlannerTaskOfferCardModel> TaskOfferModels { get; private set; } = new ObservableCollection<EventPlannerTaskOfferCardModel>();

        public EventPlannerAvailableOffersViewModel(IApplicationContext context, IOfferService offerService, ITaskOfferService taskOfferService, EventPlannerTaskDetailsViewModel taskVm) : base(context)
        {
            _offerService = offerService;
            _taskOfferService = taskOfferService;
            TaskVm = taskVm;

            Search = new DelegateCommand(() => UpdatePage(0));
            SearchQuery = string.Empty;
            Rows = 1;

            AvailableDrop = false;
        }


        public override void UpdatePage(int pageNumber)
        {
            TaskOfferModels.Clear();
            var page = _offerService.GetAvailableOffersForTask(_taskId, new OffersForTaskPageRequest { Page = pageNumber, Size = Size, SearchQuery = SearchQuery });
            foreach (var entity in page.Entities)
            {
                TaskOfferModels.Add(new EventPlannerTaskOfferCardModel { Id = entity.Id, Name = entity.Name, Description = entity.Description, Image = ImageUtil.ConvertToImage(entity.Image), IconKind = "Add", ToolTip = "Add this offer to the task's offers", ButtonAction = new DelegateCommand(() => AddOfferForTask(entity.Id)) });
            }
            OnPageFetched(page);
        }

        private void AddOfferForTask(int offerId)
        {
            var taskOffer = _taskOfferService.AddOfferForTask(_taskId, offerId);
            var toSave = _taskOfferService.Get(taskOffer.Id);
            toSave.Active = false;
            TaskVm.AddItem(toSave);
            UpdatePage(this.PaginationViewModel.Page);
            Context.Notifier.ShowInformation("Offer successfully added to the task.");
            AddedVm.UpdatePage(AddedVm.PaginationViewModel.Page);
        }
    }
}
