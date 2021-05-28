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
using UI.ViewModels.CardViewModels;
using UI.ViewModels.Interfaces;

namespace UI.ViewModels
{
    

    public class ClientRequestsViewModel : PagingViewModelBase
    {
        private readonly IClientService _clientService;

        private string _requestName;
        public string RequestName
        {
            get => _requestName;
            set
            {
                _requestName = value;
                OnPropertyChanged(nameof(RequestName));
            }
        }

        public ObservableCollection<ClientRequestCardModel> RequestModels { get; private set; } = new ObservableCollection<ClientRequestCardModel>();

        public ClientRequestsViewModel(IApplicationContext context, IClientService clientService) : base(context)
        {
            _clientService = clientService;
            RequestName = string.Empty;
            UpdatePage(0);
        }

        public override void UpdatePage(int pageNumber)
        {
            RequestModels.Clear();
            var page = _clientService.GetRequestsForClient(1, new RequestsPage { Page = pageNumber, Size = Size, RequestName = RequestName });
            foreach (var entity in page.Entities)
            {
                RequestModels.Add(new ClientRequestCardModel { Name = entity.Name});
            }
            OnPageFetched(page);
        }
    }
}
