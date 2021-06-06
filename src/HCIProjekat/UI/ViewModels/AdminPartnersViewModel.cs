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
using UI.Modals;
using UI.Services.Interfaces;

namespace UI.ViewModels
{

    public class AdminPartnerCardModel
    {
        public string Name { get; set; }
        public string PartnerType { get; set; }

        public string Address { get; set; }

        public string Route { get; set; }

        public IApplicationContext Context { get; set; }

        public ICommand Delete { get; set; }
    }

    public class AdminPartnersViewModel : PagingViewModelBase
    {
        private readonly IPartnersService _partnersService;

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

        public ICommand SearchCommand { get; set; }

        public ICommand AddPartner { get; private set; }
        public ObservableCollection<AdminPartnerCardModel> PartnerModels { get; private set; } = new ObservableCollection<AdminPartnerCardModel>();

        public AdminPartnersViewModel(IApplicationContext context, IPartnersService partnersService, IModalService modalService) : base(context)
        {
            _partnersService = partnersService;
            SearchQuery = string.Empty;
            SearchCommand = new DelegateCommand(() => UpdatePage(0));
            AddPartner = new DelegateCommand(OpenRegisterPartnerModal);
            _modalService = modalService;
            Columns = 4;
            UpdatePage(0);
        }
        private void OpenRegisterPartnerModal()
        {
            _modalService.ShowModal<AddPartnerModal>(new RegisterPartnerViewModel(Context, _partnersService));
        }

        public override void UpdatePage(int pageNumber)
        {
            PartnerModels.Clear();
            var page = _partnersService.GetPartners(new PartnersPage { Page = pageNumber, Size = Size, SearchQuery = SearchQuery });
            foreach (var entity in page.Entities)
            {

                var partnerModel = new AdminPartnerCardModel
                {
                    Name = entity.Name,
                    PartnerType = entity.Type.ToString(),
                    Address = entity.Location.Street + " " + entity.Location.StreetNumber + ", " + entity.Location.City + ", " + entity.Location.Country,
                    Route = $"PartnerOffers?partnerId={entity.Id}",
                    Context = Context
                };
                partnerModel.Delete = new DeletePartnerCommand(this, entity.Id, entity.Name, _modalService, _partnersService);
                PartnerModels.Add(partnerModel);
            }
            OnPageFetched(page);
        }
    }
}
