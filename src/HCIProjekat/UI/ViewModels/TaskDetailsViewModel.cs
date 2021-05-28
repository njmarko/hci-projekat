using Domain.Entities;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using UI.Context;
using UI.ViewModels.CardViewModels;
using Domain.Pagination.Requests;
using System.Windows;

namespace UI.ViewModels
{
    public class TaskDetailsViewModel : PagingViewModelBase
    {
        private readonly ITaskService _taskService;
        private readonly IOfferService _offerService;
        private Task _task;

        public Task Task
        {
            get { return _task; }
            set 
            { 
                _task = value;
                OnPropertyChanged(nameof(Task));
            }
        }


        public ObservableCollection<ClientTaskOfferCardModel> TaskOfferModels { get; private set; } = new ObservableCollection<ClientTaskOfferCardModel>();



        public TaskDetailsViewModel(IApplicationContext context, ITaskService taskService, IOfferService offerService) : base(context)
        {
            _taskService = taskService;
            _offerService = offerService;
            _task = _taskService.GetTask(1);
            UpdatePage(0);
        }

        public override void UpdatePage(int pageNumber)
        {
            TaskOfferModels.Clear();
            var page = _offerService.GetOffersForTask(_task.Id, new OffersForTaskPageRequest { Size = Size, Page = pageNumber});
            foreach (var entity in page.Entities)
            {
                TaskOfferModels.Add(new ClientTaskOfferCardModel 
                { 
                    PartnerName = entity.Offer.Partner.Name, 
                    Description = entity.Offer.Description,
                    OfferName = entity.Offer.Name,
                    Image = entity.Offer.Image,
                    OfferPrice = entity.Offer.Price
                });
            }
            OnPageFetched(page);

        }
    }
}
