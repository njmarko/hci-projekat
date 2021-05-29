using Domain.Services.Interfaces;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using UI.Commands;
using UI.Context;
using UI.Util;
using UI.ViewModels.Interfaces;

namespace UI.ViewModels
{
    public class CreateOfferViewModel : ViewModelBase, ISelfValidatingViewModel
    {
        private IOfferService _offerService;

        private byte[] _imageInBytes;
        public byte[] ImageInBytes
        {
            get { return _imageInBytes; }
            set { _imageInBytes = value; }
        }

        private BitmapImage _image;
        public BitmapImage Image
        {
            get { return _image; }
            set { _image = value; OnPropertyChanged(nameof(Image)); OnPropertyChanged(nameof(CanCreateOffer)); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); OnPropertyChanged(nameof(CanCreateOffer)); }
        }

        private string _price;
        public string Price
        {
            get { return _price; }
            set { _price = value; OnPropertyChanged(nameof(Price)); OnPropertyChanged(nameof(CanCreateOffer)); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(nameof(Description)); OnPropertyChanged(nameof(CanCreateOffer)); }
        }

        private int _offerId;

        public String ButtonText
        {
            get { return (_offerId != -1) ? "Save" : "Create"; }
        }

        public bool CanCreateOffer => IsValid();
        public ErrorMessageViewModel NameError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel PriceError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel DescriptionError { get; private set; } = new ErrorMessageViewModel();
        public ErrorMessageViewModel ImageError { get; private set; } = new ErrorMessageViewModel();

        public ICommand OnImageInput { get; private set; }
        public ICommand CreateOfferCommand { get; private set; }

        public CreateOfferViewModel(IApplicationContext context, IOfferService offerService, int partnerId, int offerId) : base(context)
        {
            Image = new BitmapImage(new Uri(@"pack://application:,,,/EmptyImage/EmptyImage.png", UriKind.Absolute));
            OnImageInput = new DelegateCommand(() => ImageInput());
            CreateOfferCommand = new CreateOfferCommand(this, offerService, partnerId, offerId);
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

            ImageInBytes = offer.Image;
            Image = ImageUtil.ConvertToImage(ImageInBytes);
            Name = offer.Name;
            Description = offer.Description;
            Price = offer.Price.ToString();
        }

        public bool IsValid()
        {
            bool valid = true;
            //Name
            if (string.IsNullOrEmpty(Name))
            {
                NameError.ErrorMessage = "Name is required.";
                valid = false;
            }
            else
            {
                NameError.ErrorMessage = null;
            }
            //Description
            if (string.IsNullOrEmpty(Description))
            {
                DescriptionError.ErrorMessage = "Description is required.";
                valid = false;
            }
            else
            {
                DescriptionError.ErrorMessage = null;
            }
            //Price
            if (string.IsNullOrEmpty(Price))
            {
                PriceError.ErrorMessage = "Price is required!";
                valid = false;
            }
            else if (int.TryParse(Price, out int priceNumber))
            {
                if (priceNumber <= 0)
                {
                    PriceError.ErrorMessage = "Price must be greater than 0!";
                    valid = false;
                }
                else
                {
                    PriceError.ErrorMessage = null;
                }
            }
            else
            {
                PriceError.ErrorMessage = "Price must be number!";
                valid = false;
            }
            //Image
            if (ImageInBytes == null)
            {
                ImageError.ErrorMessage = "Image is required.";
                valid = false;
            }
            else
            {
                ImageError.ErrorMessage = null;
            }

            return valid;
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
