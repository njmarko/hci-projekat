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
using UI.Context.Routers;
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

        public ICommand Edit { get; set; }

    }


    public class AdminPartnersViewModel : PagingViewModelBase
    {
        private readonly IPartnersService _partnersService;

        private readonly IModalService _modalService;

        private readonly PartnerTypeModel _typeInitial;

        private readonly IRouter _router;

        private string _query;
        public string Query
        {
            get => _query;
            set
            {
                _query = value;
                OnPropertyChanged(nameof(Query));
            }
        }

        private PartnerTypeModel _partnerType;
        public PartnerTypeModel PartnerTypeValue
        {
            get { return _partnerType; }
            set { _partnerType = value; OnPropertyChanged(nameof(PartnerTypeValue)); }
        }

        public ICommand Search { get; set; }
        public ICommand AddPartner { get; private set; }

        public ICommand Clear { get; private set; }
        public ObservableCollection<AdminPartnerCardModel> PartnerModels { get; private set; } = new ObservableCollection<AdminPartnerCardModel>();

        public ObservableCollection<PartnerTypeModel> PartnerTypeModels { get; private set; } = new ObservableCollection<PartnerTypeModel>();

        public AdminPartnersViewModel(IApplicationContext context, IPartnersService partnersService, IModalService modalService, IRouter router) : base(context)
        {
            _partnersService = partnersService;
            _router = router;
            Query = string.Empty;
            Search = new DelegateCommand(() => UpdatePage(0));
            Clear = new DelegateCommand(ClearFilters);
            AddPartner = new DelegateCommand(OpenRegisterPartnerModal);

            _modalService = modalService;
            Columns = 4;

            _typeInitial = new PartnerTypeModel { Name = "All types" };
            _partnerType = _typeInitial;

            PartnerTypeModels.Add(_typeInitial);
            PartnerTypeModels.Add(new PartnerTypeModel { Type = PartnerType.ANIMATOR, Name = "Animator" });
            PartnerTypeModels.Add(new PartnerTypeModel { Type = PartnerType.BAKERY, Name = "Bakery" });
            PartnerTypeModels.Add(new PartnerTypeModel { Type = PartnerType.CAFFEE, Name = "Coffee" });
            PartnerTypeModels.Add(new PartnerTypeModel { Type = PartnerType.CONFECTIONERY, Name = "Confectionery" });
            PartnerTypeModels.Add(new PartnerTypeModel { Type = PartnerType.MUSIC, Name = "Music" });
            PartnerTypeModels.Add(new PartnerTypeModel { Type = PartnerType.PHOTOGRAPHY, Name = "Photography" });
            PartnerTypeModels.Add(new PartnerTypeModel { Type = PartnerType.RESTAURANT, Name = "Restaurant" });
            HelpPage = "event-planner-partners";
            UpdatePage(0);
        }

        public void ClearFilters()
        {
            Query = string.Empty;
            PartnerTypeValue = _typeInitial;
            UpdatePage(0);
        }

        private void OpenRegisterPartnerModal()
        {
            _modalService.ShowModal<AddPartnerModal>(new RegisterPartnerViewModel(Context, _partnersService));
        }

        public void ShowEditPartner(int partnerId = -1)
        {
            _modalService.ShowModal<AddPartnerModal>(new RegisterPartnerViewModel(Context, _partnersService, partnerId));
        }

        public override void UpdatePage(int pageNumber)
        {
            PartnerModels.Clear();
            var page = _partnersService.GetPartners(new PartnersPage { Page = pageNumber, Size = Size, Query = Query, PartnerType = PartnerTypeValue.Type });
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
                //partnerModel.Edit = new RegisterPartnerCommand(new RegisterPartnerViewModel(Context, _partnersService, entity.Id), _partnersService, _router, Context, entity.Id);
                partnerModel.Edit = new DelegateCommand(() => ShowEditPartner(entity.Id));
                PartnerModels.Add(partnerModel);
            }
            OnPageFetched(page);
        }
    }
}
