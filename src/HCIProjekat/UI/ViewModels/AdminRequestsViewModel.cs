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
using UI.Commands;
using UI.Context;

namespace UI.ViewModels
{
    public class AdminRequestCardModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Theme { get; set; }
        public string Type { get; set; }
        public string Budget { get; set; }
        public int GuestNumber { get; set; }
        public bool BudgetFlexible { get; set; }
        public string Date { get; set; }
        public IApplicationContext Context { get; set; }
        public string Route { get; set; }
        //public ClientRequestsViewModel Vm { get; set; }
        public string CreatedBy { get; set; }

        public string AcceptedBy {get; set;}

        
    }
    public class AdminRequestsViewModel : PagingViewModelBase
    {
        //public string Header { get; set; }
        private readonly IClientService _clientService;
        private readonly IRequestService _requestService;

        private readonly DateTime _fromDateInitial = DateTime.Now.AddYears(-1);
        private readonly DateTime _toDateInitial = DateTime.Now.AddYears(1);
        private readonly RequestTypeModel _typeInitial;
        private string _query = string.Empty;
        public string Query
        {
            get { return _query; }
            set { _query = value; OnPropertyChanged(nameof(Query)); }
        }

        private DateTime _from;
        public DateTime From
        {
            get { return _from; }
            set { _from = value; OnPropertyChanged(nameof(From)); }
        }

        private DateTime _to;
        public DateTime To
        {
            get { return _to; }
            set { _to = value; OnPropertyChanged(nameof(To)); }
        }

        private RequestTypeModel _requestType;
        public RequestTypeModel RequestTypeValue
        {
            get { return _requestType; }
            set { _requestType = value; OnPropertyChanged(nameof(RequestTypeValue)); }
        }

        public ObservableCollection<AdminRequestCardModel> RequestModels { get; private set; } = new ObservableCollection<AdminRequestCardModel>();
        public ObservableCollection<RequestTypeModel> RequestTypeModels { get; private set; } = new ObservableCollection<RequestTypeModel>();
        public override ICommand Search { get; set; }
        public override ICommand Clear { get; set; }
        public AdminRequestsViewModel(IApplicationContext context, IClientService clientService, IRequestService requestService) : base(context)
        {
            _clientService = clientService;
            _requestService = requestService;
            Search = new DelegateCommand(() => UpdatePage(0));
            Clear = new DelegateCommand(ClearFilters);
            _from = _fromDateInitial;
            _to = _toDateInitial;
            // Add enums to combo box
            _typeInitial = new RequestTypeModel { Name = "All types" };
            _requestType = _typeInitial;

            RequestTypeModels.Add(_typeInitial);
            RequestTypeModels.Add(new RequestTypeModel { Type = RequestType.ANNIVERSARY, Name = "Anniversary" });
            RequestTypeModels.Add(new RequestTypeModel { Type = RequestType.BIRTHDAY, Name = "Birthday" });
            RequestTypeModels.Add(new RequestTypeModel { Type = RequestType.GRADUATION, Name = "Graduation" });
            RequestTypeModels.Add(new RequestTypeModel { Type = RequestType.PARTY, Name = "Party" });
            RequestTypeModels.Add(new RequestTypeModel { Type = RequestType.WEDDING, Name = "Wedding" });

            Rows = 2;
            Columns = 4;
            UpdatePage(0);

            HelpPage = "admin-requests";
        }
        public void ClearFilters()
        {
            From = _fromDateInitial;
            To = _toDateInitial;
            Query = string.Empty;
            RequestTypeValue = _typeInitial;
            UpdatePage(0);
        }

        public override void UpdatePage(int pageNumber)
        {
            //throw new NotImplementedException();
            RequestModels.Clear();
            var page = _requestService.GetAllRequests(new RequestsPage { Page = pageNumber, Size = Size, Query = Query, From = From, To = To, Type = RequestTypeValue.Type });
            foreach (var entity in page.Entities)
            {
                var cardModel = new AdminRequestCardModel
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Type = entity.Type.ToString().ToUpper()[0] + entity.Type.ToString().ToLower()[1..],
                    GuestNumber = entity.GuestNumber,
                    Budget = $"{entity.Budget} RSD",
                    BudgetFlexible = entity.BudgetFlexible,
                    Theme = entity.Theme,
                    Date = entity.Date.ToString("dd.MM.yyyy"),
                    Context = Context,
                    Route = $"RequestDetails?requestId={entity.Id}",
                    CreatedBy = entity.Client.Username,
                    AcceptedBy = entity.EventPlanner == null ? "No one" : entity.EventPlanner.Username
                   
                };
                //cardModel.Delete = new DeleteRequestCommand(this, entity.Id, _modalService, _requestService);
                RequestModels.Add(cardModel);
            }
            OnPageFetched(page);

        }
    }
}
