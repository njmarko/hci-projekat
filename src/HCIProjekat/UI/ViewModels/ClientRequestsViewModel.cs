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

    public class ClientRequestsViewModel : PagingViewModelBase
    {
        private readonly IClientService _clientService;

        private string _query = string.Empty;
        public string Query
        {
            get { return _query; }
            set { _query = value; OnPropertyChanged(nameof(Query)); }
        }

        public ObservableCollection<ClientRequestCardModel> RequestModels { get; private set; } = new ObservableCollection<ClientRequestCardModel>();
        public ICommand Search { get; private set; }

        public ClientRequestsViewModel(IApplicationContext context, IClientService clientService) : base(context)
        {
            _clientService = clientService;
            Search = new DelegateCommand(() => UpdatePage(0));
            UpdatePage(0);
        }

        public override void UpdatePage(int pageNumber)
        {
            RequestModels.Clear();
            var page = _clientService.GetRequestsForClient(1, new RequestsPage { Page = pageNumber, Size = Size, Query = Query });
            foreach (var entity in page.Entities)
            {
                RequestModels.Add(new ClientRequestCardModel { Name = entity.Name });
            }
            OnPageFetched(page);
        }
    }
}
