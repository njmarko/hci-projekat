using Domain.Enums;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.Commands;
using UI.Context;
using UI.ViewModels.Interfaces;

namespace UI.ViewModels
{
    public class RegisterPartnerViewModel : ViewModelBase, ISelfValidatingViewModel
    {
        // Poperties
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); OnPropertyChanged(nameof(CanRegister)); }
        }

        private PartnerType _Type;
        public PartnerType Type
        {
            get { return _Type; }
            set { _Type = value; OnPropertyChanged(nameof(Type)); OnPropertyChanged(nameof(CanRegister)); }
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
            get { return _street; }
            set { _streetNumber = value; OnPropertyChanged(nameof(StreetNumber)); OnPropertyChanged(nameof(CanRegister)); }
        }


        // Error message view models
        public ErrorMessageViewModel NameError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel TypeError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel CountryError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel CityError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel StreetError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel StreetNumberError { get; private set; } = new ErrorMessageViewModel();

        public bool CanRegister => IsValid();
        public ICommand RegisterPartnerCommand { get; private set; }

        public RegisterPartnerViewModel(IApplicationContext context, IPartnersService partnerService) : base(context)
        {
            //DateOfBirth = new DateTime(1990, 01, 01);
            RegisterPartnerCommand = new RegisterPartnerCommand(this, partnerService, context.Router);
        }

        public bool IsValid()
        {
            bool valid = true;

            //Name
            if (string.IsNullOrEmpty(Name))
            {
                CountryError.ErrorMessage = "Name cannot be empty.";
                valid = false;
            }
            else
            {
                CountryError.ErrorMessage = null;
            }
            //Type
            if (Enum.IsDefined(Type))
            {
                NameError.ErrorMessage = "Type must be selected.";
                valid = false;
            }
            else
            {
                NameError.ErrorMessage = null;
            }
            //Country
            if (string.IsNullOrEmpty(Street))
            {
                TypeError.ErrorMessage = "Country cannot be empty.";
                valid = false;
            }
            else
            {
                TypeError.ErrorMessage = null;
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
