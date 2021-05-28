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


    public class PartnerTypeModel
    {
        public string Name { get; set; }
        public PartnerType? Type { get; set; }
    }

    public class RegisterPartnerViewModel : ViewModelBase, ISelfValidatingViewModel
    {
        private readonly PartnerTypeModel _typeInitial;

        // Poperties
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); OnPropertyChanged(nameof(CanRegister)); }
        }

        private PartnerTypeModel _partnerType;
        public PartnerTypeModel PartnerTypeValue
        {
            get { return _partnerType; }
            set { _partnerType = value; OnPropertyChanged(nameof(PartnerTypeValue)); OnPropertyChanged(nameof(CanRegister)); }
        }

        private string _country;
        public string Country
        {
            get { return _country; }
            set { _country = value; OnPropertyChanged(nameof(Country)); OnPropertyChanged(nameof(CanRegister)); }
        }

        private string _city;
        public string City
        {
            get { return _city; }
            set { _city = value; OnPropertyChanged(nameof(City)); OnPropertyChanged(nameof(CanRegister)); }
        }

        private string _street;
        public string Street
        {
            get { return _street; }
            set { _street = value; OnPropertyChanged(nameof(Street)); OnPropertyChanged(nameof(CanRegister)); }
        }

        private string _streetNumber;
        public string StreetNumber
        {
            get { return _streetNumber; }
            set { _streetNumber = value; OnPropertyChanged(nameof(StreetNumber)); OnPropertyChanged(nameof(CanRegister)); }
        }



        // Error message view models
        public ErrorMessageViewModel NameError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel TypeError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel CountryError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel CityError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel StreetError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel StreetNumberError { get; private set; } = new ErrorMessageViewModel();

        public ObservableCollection<PartnerTypeModel> PartnerTypeModels { get; private set; } = new ObservableCollection<PartnerTypeModel>();

        public bool CanRegister => IsValid();
        public ICommand RegisterPartnerCommand { get; private set; }

        public RegisterPartnerViewModel(IApplicationContext context, IPartnersService partnerService) : base(context)
        {
            //DateOfBirth = new DateTime(1990, 01, 01);
            RegisterPartnerCommand = new RegisterPartnerCommand(this, partnerService, context.Router);

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
