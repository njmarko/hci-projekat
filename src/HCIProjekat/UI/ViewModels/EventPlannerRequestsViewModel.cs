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
    public class EventPlannerRequestsViewModel : PagingViewModelBase
    {
        private readonly DateTime _fromDateInitial = DateTime.Now.AddYears(-1);
        private readonly DateTime _toDateInitial = DateTime.Now.AddYears(1);
        private readonly RequestTypeModel _typeInitial;

        private readonly IRequestService _requestService;

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

        private bool _mine;
        public bool Mine
        {
            get { return _mine; }
            set { _mine = value; OnPropertyChanged(nameof(Mine)); }
        }

        private bool _onlyNew;
        public bool OnlyNew
        {
            get { return _onlyNew; }
            set { _onlyNew = value; OnPropertyChanged(nameof(OnlyNew)); }
        }

        private RequestTypeModel _requestType;
        public RequestTypeModel RequestTypeValue
        {
            get { return _requestType; }
            set { _requestType = value; OnPropertyChanged(nameof(RequestTypeValue)); }
        }

        public ObservableCollection<ClientRequestCardModel> RequestModels { get; private set; } = new ObservableCollection<ClientRequestCardModel>();
        public ObservableCollection<RequestTypeModel> RequestTypeModels { get; private set; } = new ObservableCollection<RequestTypeModel>();
        public ICommand Search { get; private set; }
        public ICommand Clear { get; private set; }

        public EventPlannerRequestsViewModel(IApplicationContext context, IRequestService requestService) : base(context)
        {
            _requestService = requestService;

            Search = new DelegateCommand(() => UpdatePage(0));
            Clear = new DelegateCommand(ClearFilters);

            // Add enums to combo box
            _typeInitial = new RequestTypeModel { Name = "All types" };
            _requestType = _typeInitial;
            _from = _fromDateInitial;
            _to = _toDateInitial;

            RequestTypeModels.Add(_typeInitial);
            RequestTypeModels.Add(new RequestTypeModel { Type = RequestType.ANNIVERSARY, Name = "Anniversary" });
            RequestTypeModels.Add(new RequestTypeModel { Type = RequestType.BIRTHDAY, Name = "Birthday" });
            RequestTypeModels.Add(new RequestTypeModel { Type = RequestType.GRADUATION, Name = "Graduation" });
            RequestTypeModels.Add(new RequestTypeModel { Type = RequestType.PARTY, Name = "Party" });
            RequestTypeModels.Add(new RequestTypeModel { Type = RequestType.WEDDING, Name = "Wedding" });

            Rows = 2;
            Columns = 4;
            UpdatePage(0);
        }

        public override void UpdatePage(int pageNumber)
        {
            RequestModels.Clear();
            var page = _requestService.GetRequestInterestingForEventPlanner(Context.Store.CurrentUser.Id, new EventPlannerRequestsPage { Page = pageNumber, Size = Size, Query = Query, Type = RequestTypeValue.Type, From = From, To = To, Mine = Mine, OnlyNew = OnlyNew });
            foreach (var entity in page.Entities)
            {
                RequestModels.Add(new ClientRequestCardModel { Name = entity.Name, Type = entity.Type.ToString(), GuestNumber = entity.GuestNumber, Budget = $"{entity.Budget} RSD", BudgetFlexible = entity.BudgetFlexible, Theme = entity.Theme, Date = entity.Date.ToString("dd.MM.yyyy"), Context = Context, Route = $"RequestDetails?requestId={entity.Id}" });
            }
            OnPageFetched(page);
        }

        public void ClearFilters()
        {
            Query = string.Empty;
            From = _fromDateInitial;
            To = _toDateInitial;
            Mine = false;
            OnlyNew = false;
            RequestTypeValue = _typeInitial;
            UpdatePage(0);
        }
    }
}
