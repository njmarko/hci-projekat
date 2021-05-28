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

    public class AdminPartnerCardModel
    {
        public string Name { get; set; }
        public string PartnerType { get; set; }
        
        public string Address { get; set; }

    }

    public class AdminPartnersViewModel : PagingViewModelBase
    {
        private readonly IPartnersService _partnersService;

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
        public ObservableCollection<AdminPartnerCardModel> PartnerModels { get; private set; } = new ObservableCollection<AdminPartnerCardModel>();

        public AdminPartnersViewModel(IApplicationContext context, IPartnersService partnersService) : base(context)
        {
            _partnersService = partnersService;
            SearchQuery = string.Empty;
            SearchCommand = new DelegateCommand(() => UpdatePage(0));
            Columns = 4;
            UpdatePage(0);
        }

        public override void UpdatePage(int pageNumber)
        {
            PartnerModels.Clear();
            var page = _partnersService.GetPartners(new PartnersPage { Page = pageNumber, Size = Size, SearchQuery = SearchQuery });
            foreach (var entity in page.Entities)
            {

                PartnerModels.Add(new AdminPartnerCardModel
                {
                    Name = entity.Name,
                    PartnerType = entity.Type.ToString(),
                    Address = entity.Location.Street + " " + entity.Location.StreetNumber + ", " + entity.Location.City + ", " + entity.Location.Country
                });
            }
            OnPageFetched(page);
        }
    }
}
