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
using UI.Services.Interfaces;

namespace UI.ViewModels
{

    public class AdminClientCardModel
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string DateOfBirth { get; set; }
        public int ActiveRequests { get; set; }
        public int CompletedRequests { get; set; }
        public ICommand Delete { get; set; }

    }

    public class AdminClientsViewModel : PagingViewModelBase
    {
        private readonly IClientService _clientService;

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
        public ObservableCollection<AdminClientCardModel> ClientModels { get; private set; } = new ObservableCollection<AdminClientCardModel>();

        public AdminClientsViewModel(IApplicationContext context, IClientService clientService, IModalService modalService) : base(context)
        {
            _clientService = clientService;
            SearchQuery = string.Empty;
            SearchCommand = new DelegateCommand(() => UpdatePage(0));
            _modalService = modalService;
            Columns = 4;
            UpdatePage(0);
        }

        public override void UpdatePage(int pageNumber)
        {
            ClientModels.Clear();
            var page = _clientService.GetClients(new ClientsPage { Page = pageNumber, Size = Size, SearchQuery = SearchQuery });
            foreach (var entity in page.Entities)
            {

                var clientModel = new AdminClientCardModel
                {
                    Name = entity.FirstName + " " + entity.LastName,
                    Username = entity.Username,
                    DateOfBirth = entity.DateOfBirth.ToShortDateString(),
                    ActiveRequests = 12,
                    CompletedRequests = 150
                };
                clientModel.Delete = new DeleteClientCommand(this, entity.Id, entity.Username, _modalService, _clientService);
                ClientModels.Add(clientModel);
            }
            OnPageFetched(page);
        }
    }
}
