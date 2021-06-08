using Domain.Entities;
using Domain.Pagination.Requests;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UI.Commands;
using UI.Context;
using UI.Modals;
using UI.Services.Interfaces;

namespace UI.ViewModels
{

    public class AdminAdminsCardModel
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string DateOfBirth { get; set; }

        public Visibility Deletable { get; set; }
        public ICommand Delete { get; set; }

    }

    public class AdminAdminsViewModel : PagingViewModelBase
    {
        private readonly IAdminService _adminService;

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

        public ICommand Search { get; set; }

        public ICommand AddAdmin { get; private set; }
        public ICommand Clear { get; private set; }
        public ObservableCollection<AdminAdminsCardModel> AdminModels { get; private set; } = new ObservableCollection<AdminAdminsCardModel>();

        public AdminAdminsViewModel(IApplicationContext context, IAdminService adminService, IModalService modalService) : base(context)
        {
            _adminService = adminService;
            Query = string.Empty;
            Search = new DelegateCommand(() => UpdatePage(0));
            Clear = new DelegateCommand(ClearFilters);
            _bornBefore = _bornBeforeInitial;
            _bornAfter = _bornAfterInitial;
            AddAdmin = new DelegateCommand(OpenRegisterAdminModal);
            _modalService = modalService;
            Columns = 4;
            UpdatePage(0);
        }

        private void OpenRegisterAdminModal()
        {
            _modalService.ShowModal<AddAdminModal>(new RegisterAdminViewModel(Context, _adminService));
        }

        public void ClearFilters()
        {
            BornAfter = _bornAfterInitial;
            BornBefore = _bornBeforeInitial;
            Query = string.Empty;
            UpdatePage(0);
        }

        public override void UpdatePage(int pageNumber)
        {
            AdminModels.Clear();
            var page = _adminService.GetAdmins(new AdminsPage { Page = pageNumber, Size = Size, Query = Query, BornAfter = BornAfter, BornBefore = BornBefore });
            foreach (var entity in page.Entities)
            {
                var adminModel = new AdminAdminsCardModel
                {
                    Name = entity.FirstName + " " + entity.LastName,
                    Username = entity.Username,
                    DateOfBirth = entity.DateOfBirth.ToShortDateString(),
                    Deletable = (entity is not SuperAdmin && entity.Id != Context.Store.CurrentUser.Id && Context.Store.CurrentUser is SuperAdmin) ? Visibility.Visible : Visibility.Hidden,
                };
                adminModel.Delete = new DeleteAdminCommand(this, entity.Id, entity.Username, _modalService, _adminService);
                AdminModels.Add(adminModel);
            }
            OnPageFetched(page);
        }
    }
}
