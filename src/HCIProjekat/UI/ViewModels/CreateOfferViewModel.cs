using Domain.Entities;
using Domain.Enums;
using Domain.Services.Interfaces;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ToastNotifications.Messages;
using UI.Commands;
using UI.Context;
using UI.CustomAttributes;
using UI.Modals;
using UI.Services.Interfaces;
using UI.Util;
using UI.ViewModels.Interfaces;

namespace UI.ViewModels
{
    public class ServiceTypeModel
    {
        public string Name { get; set; }
        public ServiceType? Type { get; set; }
    }

    public class CreateOfferViewModel : ValidationModel<CreateOfferViewModel>
    {
        private readonly IOfferService _offerService;
        private readonly IModalService _modalService;
        private readonly ISeatingLayoutService _seatingLayoutService;
        private SeatingLayout _seatingLayout;

        private byte[] _imageInBytes;
        [ValidationField]
        public byte[] ImageInBytes
        {
            get { return _imageInBytes; }
            set { _imageInBytes = value; OnPropertyChanged(nameof(ImageInBytes)); }
        }

        private BitmapImage _image;
        public BitmapImage Image
        {
            get { return _image; }
            set { _image = value; OnPropertyChanged(nameof(Image)); OnPropertyChanged(nameof(CanCreateOffer)); }
        }

        private string _name;
        [ValidationField]
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); OnPropertyChanged(nameof(CanCreateOffer)); }
        }

        private string _price;
        [ValidationField]
        public string Price
        {
            get { return _price; }
            set { _price = value; OnPropertyChanged(nameof(Price)); OnPropertyChanged(nameof(CanCreateOffer)); }
        }

        private ServiceTypeModel _offerType;
        public ServiceTypeModel OfferTypeValue
        {
            get { return _offerType; }
            set { _offerType = value; OnPropertyChanged(nameof(OfferTypeValue)); OnPropertyChanged(nameof(CanCreateOffer)); OnPropertyChanged(nameof(IsLocation)); }
        }

        private string _description;
        [ValidationField]
        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(nameof(Description)); OnPropertyChanged(nameof(CanCreateOffer)); }
        }

        private int _offerId;

        public string ButtonText
        {
            get { return (_offerId != -1) ? "Save" : "Create"; }
        }

        public bool IsLocation => _offerType.Type.Value == ServiceType.LOCATION;

        public ObservableCollection<ServiceTypeModel> OfferTypeModels { get; private set; } = new ObservableCollection<ServiceTypeModel>();

        public bool CanCreateOffer => IsValid();
        public ErrorMessageViewModel NameError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel PriceError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel DescriptionError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel ImageError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel LayoutError { get; private set; } = new ErrorMessageViewModel();

        public ICommand ShowEditSeatingDialogModal { get; private set; }
        public ICommand OnImageInput { get; private set; }
        public ICommand CreateOfferCommand { get; private set; }

        public string Help {get; private set;} = "add-partner-offer";

        public CreateOfferViewModel(PartnerOffersViewModel partnerVm, IApplicationContext context, IOfferService offerService, ISeatingLayoutService seatingLayoutService, IModalService modalService, int partnerId, int offerId) : base(context)
        {
            _offerService = offerService;
            _modalService = modalService;
            _seatingLayoutService = seatingLayoutService;
            _offerId = offerId;

            OfferTypeValue = new ServiceTypeModel { Name = "Location", Type = ServiceType.LOCATION };
            OfferTypeModels.Add(OfferTypeValue);
            OfferTypeModels.Add(new ServiceTypeModel { Name = "Catering", Type = ServiceType.CATERING });
            OfferTypeModels.Add(new ServiceTypeModel { Name = "Music", Type = ServiceType.MUSIC });
            OfferTypeModels.Add(new ServiceTypeModel { Name = "Photography", Type = ServiceType.PHOTOGRAPHY });
            OfferTypeModels.Add(new ServiceTypeModel { Name = "Animator", Type = ServiceType.ANIMATOR });

            if (offerId != -1)
            {
                FetchOffer();
                Help = "edit-partner-offer";
            }
            else
            {
                Image = new BitmapImage(new Uri(@"pack://application:,,,/EmptyImage/EmptyImage.png", UriKind.Absolute));
                _seatingLayout = new SeatingLayout();
            }


            OnImageInput = new DelegateCommand(ImageInput);
            CreateOfferCommand = new CreateOfferCommand(partnerVm, this, offerService, seatingLayoutService, partnerId, offerId, _seatingLayout);
            ShowEditSeatingDialogModal = new DelegateCommand(ShowSeatingDialogModal);
        }

        private void FetchOffer()
        {
            var offer = _offerService.Get(_offerId);

            ImageInBytes = offer.Image;
            Image = ImageUtil.ConvertToImage(ImageInBytes);
            Name = offer.Name;
            Description = offer.Description;
            Price = offer.Price.ToString();
            var enumName = offer.OfferType.ToString().First().ToString().ToUpper() + offer.OfferType.ToString().ToLower()[1..];
            OfferTypeValue = OfferTypeModels.Where(o => o.Type.HasValue && o.Type.Value == offer.OfferType).First();

            if (offer.OfferType == ServiceType.LOCATION)
            {
                _seatingLayout = _offerService.GetOfferSeatingLayout(_offerId);
            }
        }

        public bool IsValid()
        {
            bool valid = true;
            //Name
            if (string.IsNullOrEmpty(Name) && IsDirty(nameof(Name)))
            {
                NameError.ErrorMessage = "Name is required.";
                valid = false;
            }
            else
            {
                NameError.ErrorMessage = null;
            }
            //Description
            if (string.IsNullOrEmpty(Description) && IsDirty(nameof(Description)))
            {
                DescriptionError.ErrorMessage = "Description is required.";
                valid = false;
            }
            else
            {
                DescriptionError.ErrorMessage = null;
            }
            //Price
            if (string.IsNullOrEmpty(Price) && IsDirty(nameof(Price)))
            {
                PriceError.ErrorMessage = "Price is required!";
                valid = false;
            }
            else if (int.TryParse(Price, out int priceNumber))
            {
                if (priceNumber <= 0 && IsDirty(nameof(Price)))
                {
                    PriceError.ErrorMessage = "Price must be greater than 0!";
                    valid = false;
                }
                else
                {
                    PriceError.ErrorMessage = null;
                }
            }
            else if (IsDirty(nameof(Price)))
            {
                PriceError.ErrorMessage = "Price must be number!";
                valid = false;
            }
            //Image
            if (ImageInBytes == null && IsDirty(nameof(ImageInBytes)))
            {
                ImageError.ErrorMessage = "Image is required.";
                valid = false;
            }
            else
            {
                ImageError.ErrorMessage = null;
            }
            //Seating layout
            if (OfferTypeValue.Type.HasValue && OfferTypeValue.Type.Value == ServiceType.LOCATION && _seatingLayout == null)
            {
                LayoutError.ErrorMessage = "Seating layout is required for location offers.";
                valid = false;
            }
            else
            {
                LayoutError.ErrorMessage = null;
            }

            return valid && AllDirty();
        }

        private void ShowSeatingDialogModal()
        {
            if (_modalService.ShowModal<OfferSeatingLayoutModal>(new CreateOfferSeatingLayoutViewModel(this, Context, _seatingLayoutService, _seatingLayout)))
            {
                Context.Notifier.ShowInformation("Seating layout has been updated.");
            }
        }

        private void ImageInput()
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp"
            };
            if (dialog.ShowDialog() == true)
            {
                var newImage = ImageUtil.ReadFromFile(dialog.FileName);
                ImageInBytes = newImage;
                Image = ImageUtil.ConvertToImage(newImage);
            }
        }
    }
}
