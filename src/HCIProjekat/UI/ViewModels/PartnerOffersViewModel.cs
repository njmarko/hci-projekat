using Domain.Entities;
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
using System.Windows.Media.Imaging;
using ToastNotifications.Messages;
using UI.Commands;
using UI.Context;
using UI.Modals;
using UI.Services.Interfaces;
using UI.Util;

namespace UI.ViewModels
{
    public class PartnerOfferCardModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public BitmapImage Image { get; set; }
        public PartnerOffersViewModel PartnerOffersVm { get; set; }

        public ICommand EditOffer { get; private set; }
        public ICommand DeleteOffer { get; private set; }

        public PartnerOfferCardModel()
        {
            EditOffer = new DelegateCommand(() => PartnerOffersVm.OpenOfferModal(PartnerOffersVm.PartnerId, Id));
            DeleteOffer = new DelegateCommand(() => PartnerOffersVm.DeleteOffer(Id));
        }

    }

    public class PartnerOffersViewModel : UndoModelBase<Offer>
    {
        private readonly IOfferService _offerService;

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

        private ServiceTypeModel _offerType;
        public ServiceTypeModel OfferTypeValue
        {
            get { return _offerType; }
            set { _offerType = value; OnPropertyChanged(nameof(OfferTypeValue)); }
        }

        public int PartnerId { get; set; }

        public ICommand AddOffer { get; private set; }
        public ICommand Search { get; private set; }

        public ObservableCollection<ServiceTypeModel> OfferTypeModels { get; private set; } = new ObservableCollection<ServiceTypeModel>();
        public ObservableCollection<PartnerOfferCardModel> OfferModels { get; private set; } = new ObservableCollection<PartnerOfferCardModel>();

        public PartnerOffersViewModel(IApplicationContext context, IOfferService offerService, IModalService modalService) : base(context, offerService, modalService)
        {
            OfferTypeValue = new ServiceTypeModel { Name = "All types", Type = null };
            OfferTypeModels.Add(OfferTypeValue);
            OfferTypeModels.Add(new ServiceTypeModel { Name = "Location", Type = ServiceType.LOCATION });
            OfferTypeModels.Add(new ServiceTypeModel { Name = "Catering", Type = ServiceType.CATERING });
            OfferTypeModels.Add(new ServiceTypeModel { Name = "Music", Type = ServiceType.MUSIC });
            OfferTypeModels.Add(new ServiceTypeModel { Name = "Photography", Type = ServiceType.PHOTOGRAPHY });
            OfferTypeModels.Add(new ServiceTypeModel { Name = "Animator", Type = ServiceType.ANIMATOR });

            _offerService = offerService;
            SearchQuery = string.Empty;
            UpdatePage(0);

            AddOffer = new DelegateCommand(() => OpenOfferModal(1, -1));
            Search = new DelegateCommand(() => UpdatePage(0));

            _modalService = modalService;
            PartnerId = 1;
        }

        public override void UpdatePage(int pageNumber)
        {
            OfferModels.Clear();
            var page = _offerService.GetOffersForPartner(1, new OffersPage { Page = pageNumber, Size = Size, SearchQuery = SearchQuery, OfferType = OfferTypeValue.Type });
            foreach (var entity in page.Entities)
            {
                OfferModels.Add(new PartnerOfferCardModel { Id = entity.Id, Name = entity.Name, Description = entity.Description, Image = ImageUtil.ConvertToImage(entity.Image), PartnerOffersVm = this });
            }
            OnPageFetched(page);
        }

        public void OpenOfferModal(int partnerId, int offerId)
        {
            var ok = _modalService.ShowModal<OfferModal>(new CreateOfferViewModel(this, Context, _offerService, _modalService, partnerId, offerId));
            if (ok)
            {
                Context.Notifier.ShowInformation($"Offer successfully {((offerId != -1) ? "updated" : "created" )}");
            }
            UpdatePage(0);
        }

        public void DeleteOffer(int offerId)
        {
            var ok = _modalService.ShowConfirmationDialog("Are you sure you want to delete selected offer?");
            if (ok)
            {
                AddItem(_offerService.Get(offerId));
                _offerService.Delete(offerId);
                Context.Notifier.ShowInformation($"Offer successfully deleted");
            }
            UpdatePage(0);
        }
    }
}
