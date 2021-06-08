using Domain.Entities;
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

    public class AdminClientsViewModel : UndoModelBase<Client>
    {
        private readonly IClientService _clientService;

        private readonly IModalService _modalService;

        private readonly DateTime _bornBeforeInitial = DateTime.Now.AddDays(1);
        private readonly DateTime _bornAfterInitial = DateTime.Now.AddYears(-100);

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

        private DateTime _bornBefore;
        public DateTime BornBefore
        {
            get { return _bornBefore; }
            set { _bornBefore = value; OnPropertyChanged(nameof(BornBefore)); }
        }

        private DateTime _bornAfter;
        public DateTime BornAfter
        {
            get { return _bornAfter; }
            set { _bornAfter = value; OnPropertyChanged(nameof(BornAfter)); }
        }

        private bool _hasActiveRequests = false;
        public bool HasActiveRequests
        {
            get => _hasActiveRequests;
            set
            {
                _hasActiveRequests = value;
                OnPropertyChanged(nameof(HasActiveRequests));
            }
        }

        public ICommand Search { get; set; }

        public ICommand Clear { get; private set; }
        public ObservableCollection<AdminClientCardModel> ClientModels { get; private set; } = new ObservableCollection<AdminClientCardModel>();

        public AdminClientsViewModel(IApplicationContext context, IClientService clientService, IModalService modalService) : base(context, clientService, modalService)
        {
            _clientService = clientService;
            Query = string.Empty;
            Search = new DelegateCommand(() => UpdatePage(0));
            Clear = new DelegateCommand(ClearFilters);
            _bornBefore = _bornBeforeInitial;
            _bornAfter = _bornAfterInitial;
            _modalService = modalService;
            Columns = 4;
            UpdatePage(0);
        }

        public void ClearFilters()
        {
            BornAfter = _bornAfterInitial;
            BornBefore = _bornBeforeInitial;
            Query = string.Empty;
            HasActiveRequests = false;
            UpdatePage(0);
        }

        public override void UpdatePage(int pageNumber)
        {
            ClientModels.Clear();
            var page = _clientService.GetClients(new ClientsPage { Page = pageNumber, Size = Size, Query = Query, BornAfter = BornAfter, BornBefore = BornBefore, HasActiveRequests = HasActiveRequests });
            foreach (var entity in page.Entities)
            {

                var clientModel = new AdminClientCardModel
                {
                    Name = entity.FirstName + " " + entity.LastName,
                    Username = entity.Username,
                    DateOfBirth = entity.DateOfBirth.ToShortDateString(),
                    ActiveRequests = entity.MyRequests.Count(r => r.Date < DateTime.Now),
                    CompletedRequests = entity.MyRequests.Count(r => r.Date >= DateTime.Now),
                };
                clientModel.Delete = new DeleteClientCommand(this, entity.Id, entity.Username, _modalService, _clientService);
                ClientModels.Add(clientModel);
            }
            OnPageFetched(page);
        }
    }
}
