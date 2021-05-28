using Domain.Pagination.Requests;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using UI.Context;
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

        public ObservableCollection<PartnerOfferCardModel> OfferModels { get; private set; } = new ObservableCollection<PartnerOfferCardModel>();

        public PartnerOffersViewModel(IApplicationContext context, IOfferService offerService) : base(context)
        {
            _offerService = offerService;
            OfferName = string.Empty;
            UpdatePage(0);
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
    }
}
