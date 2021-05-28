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
using UI.ViewModels.Interfaces;

namespace UI.ViewModels
{
    public class ClientRequestCardModel
    {
        public string Name { get; set; }
    }

    public class RequestTypeModel
    {
        public string Name { get; set; }
        public RequestType? Type { get; set; }
    }

    public class ClientRequestsViewModel : PagingViewModelBase
    {
        private readonly DateTime _fromDateInitial = DateTime.Now.AddYears(-1);
        private readonly DateTime _toDateInitial = DateTime.Now.AddYears(1);
        private readonly RequestTypeModel _typeInitial;

        private readonly IClientService _clientService;

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

        public ObservableCollection<ClientRequestCardModel> RequestModels { get; private set; } = new ObservableCollection<ClientRequestCardModel>();
        public ObservableCollection<RequestTypeModel> RequestTypeModels { get; private set; } = new ObservableCollection<RequestTypeModel>();
        public ICommand Search { get; private set; }
        public ICommand Clear { get; private set; }

        public ClientRequestsViewModel(IApplicationContext context, IClientService clientService) : base(context)
        {
            Search = new DelegateCommand(() => UpdatePage(0));
            Clear = new DelegateCommand(ClearFilters);

            _clientService = clientService;
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

            UpdatePage(0);
        }

        public override void UpdatePage(int pageNumber)
        {
            RequestModels.Clear();
            var page = _clientService.GetRequestsForClient(1, new RequestsPage { Page = pageNumber, Size = Size, Query = Query, From = From, To = To, Type = RequestTypeValue.Type });
            foreach (var entity in page.Entities)
            {
                RequestModels.Add(new ClientRequestCardModel { Name = entity.Name });
            }
            OnPageFetched(page);
        }

        public void ClearFilters()
        {
            From = _fromDateInitial;
            To = _toDateInitial;
            Query = string.Empty;
            RequestTypeValue = _typeInitial;
            UpdatePage(0);
        }
    }
}
