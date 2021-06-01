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
using UI.Commands;
using UI.Context;
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
        private IOfferService _offerService;

        private byte[] _imageInBytes;
        public byte[] ImageInBytesField
        {
            get { return _imageInBytes; }
            set { _imageInBytes = value; OnPropertyChanged(nameof(ImageInBytesField)); }
        }

        private BitmapImage _image;
        public BitmapImage Image
        {
            get { return _image; }
            set { _image = value; OnPropertyChanged(nameof(Image)); OnPropertyChanged(nameof(CanCreateOffer)); }
        }

        private string _name;
        public string NameField
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(NameField)); OnPropertyChanged(nameof(CanCreateOffer)); }
        }

        private string _price;
        public string PriceField
        {
            get { return _price; }
            set { _price = value; OnPropertyChanged(nameof(PriceField)); OnPropertyChanged(nameof(CanCreateOffer)); }
        }

        private ServiceTypeModel _offerType;
        public ServiceTypeModel OfferTypeValue
        {
            get { return _offerType; }
            set { _offerType = value; OnPropertyChanged(nameof(OfferTypeValue)); }
        }

        private string _description;
        public string DescriptionField
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(nameof(DescriptionField)); OnPropertyChanged(nameof(CanCreateOffer)); }
        }

        private int _offerId;

        public String ButtonText
        {
            get { return (_offerId != -1) ? "Save" : "Create"; }
        }

        public ObservableCollection<ServiceTypeModel> OfferTypeModels { get; private set; } = new ObservableCollection<ServiceTypeModel>();

        public bool CanCreateOffer => IsValid();
        public ErrorMessageViewModel NameError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel PriceError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel DescriptionError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel ImageError { get; private set; } = new ErrorMessageViewModel();

        public ICommand OnImageInput { get; private set; }
        public ICommand CreateOfferCommand { get; private set; }

        public CreateOfferViewModel(PartnerOffersViewModel partnerVm, IApplicationContext context, IOfferService offerService, IModalService modalService, int partnerId, int offerId) : base(context)
        {
            OfferTypeValue = new ServiceTypeModel { Name = "Location", Type = ServiceType.LOCATION };
            OfferTypeModels.Add(OfferTypeValue);
            OfferTypeModels.Add(new ServiceTypeModel { Name = "Catering", Type = ServiceType.CATERING });
            OfferTypeModels.Add(new ServiceTypeModel { Name = "Music", Type = ServiceType.MUSIC });
            OfferTypeModels.Add(new ServiceTypeModel { Name = "Photography", Type = ServiceType.PHOTOGRAPHY });
            OfferTypeModels.Add(new ServiceTypeModel { Name = "Animator", Type = ServiceType.ANIMATOR });

            Image = new BitmapImage(new Uri(@"pack://application:,,,/EmptyImage/EmptyImage.png", UriKind.Absolute));

            OnImageInput = new DelegateCommand(() => ImageInput());
            CreateOfferCommand = new CreateOfferCommand(partnerVm, this, offerService, modalService, partnerId, offerId);

            _offerService = offerService;
            _offerId = offerId;
            if (offerId != -1)
            {
                FetchOffer();
            }
        }

        private void FetchOffer()
        {
            var offer = _offerService.Get(_offerId);

            ImageInBytesField = offer.Image;
            Image = ImageUtil.ConvertToImage(ImageInBytesField);
            NameField = offer.Name;
            DescriptionField = offer.Description;
            PriceField = offer.Price.ToString();
            var enumName = offer.OfferType.ToString().First().ToString().ToUpper() + offer.OfferType.ToString().ToLower()[1..];
            OfferTypeValue = OfferTypeModels.Where(o => o.Name == enumName).First();
        }

        public bool IsValid()
        {
            bool valid = true;
            //Name
            if (string.IsNullOrEmpty(NameField) && IsDirty(nameof(NameField)))
            {
                NameError.ErrorMessage = "Name is required.";
                valid = false;
            }
            else
            {
                NameError.ErrorMessage = null;
            }
            //Description
            if (string.IsNullOrEmpty(DescriptionField) && IsDirty(nameof(DescriptionField)))
            {
                DescriptionError.ErrorMessage = "Description is required.";
                valid = false;
            }
            else
            {
                DescriptionError.ErrorMessage = null;
            }
            //Price
            if (string.IsNullOrEmpty(PriceField) && IsDirty(nameof(PriceField)))
            {
                PriceError.ErrorMessage = "Price is required!";
                valid = false;
            }
            else if (int.TryParse(PriceField, out int priceNumber))
            {
                if (priceNumber <= 0 && IsDirty(nameof(PriceField)))
                {
                    PriceError.ErrorMessage = "Price must be greater than 0!";
                    valid = false;
                }
                else
                {
                    PriceError.ErrorMessage = null;
                }
            }
            else if (IsDirty(nameof(PriceField)))
            {
                PriceError.ErrorMessage = "Price must be number!";
                valid = false;
            }
            //Image
            if (ImageInBytesField == null && IsDirty(nameof(ImageInBytesField)))
            {
                ImageError.ErrorMessage = "Image is required.";
                valid = false;
            }
            else
            {
                ImageError.ErrorMessage = null;
            }

            return valid && AllDirty();
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
                ImageInBytesField = newImage;
                Image = ImageUtil.ConvertToImage(newImage);
            }
        }
    }
}
