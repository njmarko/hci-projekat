using Domain.Pagination.Requests;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Context;

namespace UI.ViewModels
{

    public class AdminClientCardModel
    {
        public string Name { get; set; }
    }

    public class AdminClientsViewModel : PagingViewModelBase
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

        public ObservableCollection<AdminClientCardModel> RequestModels { get; private set; } = new ObservableCollection<AdminClientCardModel>();

        public AdminClientsViewModel(IApplicationContext context, IClientService clientService) : base(context)
        {
            _clientService = clientService;
            RequestName = string.Empty;
            UpdatePage(0);
        }

        public override void UpdatePage(int pageNumber)
        {
            RequestModels.Clear();
            var page = _clientService.GetClients(new RequestsPage { Page = pageNumber, Size = Size, RequestName = RequestName });
            foreach (var entity in page.Entities)
            {
                RequestModels.Add(new AdminClientCardModel { Name = entity.FirstName });
            }
            OnPageFetched(page);
        }
    }
}
