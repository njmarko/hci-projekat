using GalaSoft.MvvmLight.Command;
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

namespace UI.ViewModels
{
    public class CreateOfferViewModel : ViewModelBase
    {
        private byte[] _imageInBytes;
        private byte[] ImageInBytes
        {
            get { return _imageInBytes; }
            set { _imageInBytes = value; }
        }

        private BitmapImage _image;
        public BitmapImage Image
        {
            get { return _image; }
            set { _image = value; OnPropertyChanged(nameof(Image)); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        private int _price;
        public int Price
        {
            get { return _price; }
            set { _price = value; OnPropertyChanged(nameof(Price)); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(nameof(Description)); }
        }

        public ICommand OnImageInput { get; set; }

        public CreateOfferViewModel(IApplicationContext context) : base(context)
        {
            Image = new BitmapImage(new Uri(@"pack://application:,,,/EmptyImage/EmptyImage.png", UriKind.Absolute));
            OnImageInput = new RelayCommand<byte[]>(ImageInput);
        }

        private void ImageInput(byte[] newImage)
        {
            ImageInBytes = newImage;
            Image = ConvertToImage(newImage);
        }

        private BitmapImage ConvertToImage(byte[] imageArray)
        {
            using (var ms = new MemoryStream(imageArray))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; 
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }
    }
}
