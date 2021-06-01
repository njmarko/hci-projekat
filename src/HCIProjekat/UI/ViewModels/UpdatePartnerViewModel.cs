using Domain.Entities;
using Domain.Enums;
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

    public class UpdatePartnerViewModel : ViewModelBase, ISelfValidatingViewModel
    {

        private readonly IPartnersService _partnerService;

        public bool CanUpdate => IsValid();

        private Partner _partner;

        public Partner Partner
        {
            get { return _partner; }
            set
            {
                _partner = value;
                OnPropertyChanged(nameof(Partner));
                OnPropertyChanged(nameof(CanUpdate));

            }
        }

        private readonly PartnerTypeModel _typeInitial;

        // Poperties
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); OnPropertyChanged(nameof(CanUpdate)); }
        }

        private PartnerTypeModel _partnerType;
        public PartnerTypeModel PartnerTypeValue
        {
            get { return _partnerType; }
            set { _partnerType = value; OnPropertyChanged(nameof(PartnerTypeValue)); OnPropertyChanged(nameof(CanUpdate)); }
        }

        private string _country;
        public string Country
        {
            get { return _country; }
            set { _country = value; OnPropertyChanged(nameof(Country)); OnPropertyChanged(nameof(CanUpdate)); }
        }

        private string _city;
        public string City
        {
            get { return _city; }
            set { _city = value; OnPropertyChanged(nameof(City)); OnPropertyChanged(nameof(CanUpdate)); }
        }

        private string _street;
        public string Street
        {
            get { return _street; }
            set { _street = value; OnPropertyChanged(nameof(Street)); OnPropertyChanged(nameof(CanUpdate)); }
        }

        private string _streetNumber;
        public string StreetNumber
        {
            get { return _streetNumber; }
            set { _streetNumber = value; OnPropertyChanged(nameof(StreetNumber)); OnPropertyChanged(nameof(CanUpdate)); }
        }


        public ICommand Update { get; private set; }

        // Error message view models
        public ErrorMessageViewModel NameError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel TypeError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel CountryError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel CityError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel StreetError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel StreetNumberError { get; private set; } = new ErrorMessageViewModel();

        public ObservableCollection<PartnerTypeModel> PartnerTypeModels { get; private set; } = new ObservableCollection<PartnerTypeModel>();

        public UpdatePartnerViewModel(IApplicationContext context, IPartnersService partnerService) : base(context)
        {
            _partnerService = partnerService;
            Partner = Context.Store.;
            Username = _user.Username;
            FirstName = _user.FirstName;
            LastName = _user.LastName;
            DateOfBirth = _user.DateOfBirth;
            Update = new UpdatePartnerCommand(this, partnerService);

            PartnerTypeModels.Add(_typeInitial);
            PartnerTypeModels.Add(new PartnerTypeModel { Type = PartnerType.ANIMATOR, Name = "Animator" });
            PartnerTypeModels.Add(new PartnerTypeModel { Type = PartnerType.BAKERY, Name = "Bakery" });
            PartnerTypeModels.Add(new PartnerTypeModel { Type = PartnerType.CAFFEE, Name = "Coffee" });
            PartnerTypeModels.Add(new PartnerTypeModel { Type = PartnerType.CONFECTIONERY, Name = "Confectionery" });
            PartnerTypeModels.Add(new PartnerTypeModel { Type = PartnerType.MUSIC, Name = "Music" });
            PartnerTypeModels.Add(new PartnerTypeModel { Type = PartnerType.PHOTOGRAPHY, Name = "Photography" });
            PartnerTypeModels.Add(new PartnerTypeModel { Type = PartnerType.RESTAURANT, Name = "Restaurant" });

        }


        public bool IsValid()
        {
            bool valid = true;

            //Name
            if (string.IsNullOrEmpty(Name))
            {
                NameError.ErrorMessage = "Name cannot be empty.";
                valid = false;
            }
            else
            {
                NameError.ErrorMessage = null;
            }
            //Type
            if (PartnerTypeValue == _typeInitial)
            {
                TypeError.ErrorMessage = "Type must be selected.";
                valid = false;
            }
            else
            {
                TypeError.ErrorMessage = null;
            }
            //Country
            if (string.IsNullOrEmpty(Country))
            {
                CountryError.ErrorMessage = "Country cannot be empty.";
                valid = false;
            }
            else
            {
                CountryError.ErrorMessage = null;
            }
            //City
            if (string.IsNullOrEmpty(City))
            {
                CityError.ErrorMessage = "City cannot be empty.";
                valid = false;
            }
            else
            {
                CityError.ErrorMessage = null;
            }
            //Street
            if (string.IsNullOrEmpty(Street))
            {
                StreetError.ErrorMessage = "Street cannot be empty.";
                valid = false;
            }
            else
            {
                StreetError.ErrorMessage = null;
            }
            //Street number
            if (string.IsNullOrEmpty(StreetNumber))
            {
                StreetNumberError.ErrorMessage = "Street number cannot be empty.";
                valid = false;
            }
            else
            {
                StreetNumberError.ErrorMessage = null;
            }
            return valid;
        }

    }
}
