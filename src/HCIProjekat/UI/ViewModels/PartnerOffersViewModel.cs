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
        public string Name { get; set; }
        public string Description { get; set; }
        public BitmapImage Image { get; set; }
    }

    class PartnerOffersViewModel : PagingViewModelBase
    {
        private readonly IOfferService _offerService;

        private readonly IModalService _modalService;

        private string _offerName;
        public string OfferName
        {
            get => _offerName;
            set
            {
                _offerName = value;
                OnPropertyChanged(nameof(OfferName));
            }
        }

        public ICommand AddOffer { get; private set; }

        public ObservableCollection<PartnerOfferCardModel> OfferModels { get; private set; } = new ObservableCollection<PartnerOfferCardModel>();

        public PartnerOffersViewModel(IApplicationContext context, IOfferService offerService, IModalService modalService) : base(context)
        {
            _offerService = offerService;
            OfferName = string.Empty;
            UpdatePage(0);
            AddOffer = new DelegateCommand(() => OpenOfferModal());
            _modalService = modalService;
        }

        public override void UpdatePage(int pageNumber)
        {
            OfferModels.Clear();
            var page = _offerService.GetOffersForPartner(1, new OffersPage { Page = pageNumber, Size = Size, OfferName = OfferName });
            foreach (var entity in page.Entities)
            {
                OfferModels.Add(new PartnerOfferCardModel { Name = entity.Name, Description = entity.Description, Image = ImageUtil.ConvertToImage(entity.Image) });
            }
            OnPageFetched(page);
        }

        private void OpenOfferModal()
        {
            _modalService.ShowModal<OfferModal>(new CreateOfferViewModel(Context, _offerService));
            UpdatePage(0);
        }
    }
}
