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
        private BitmapImage _image;
        public BitmapImage Image
        {
            get { return _image; }
            set { _image = value; OnPropertyChanged(nameof(Image)); }
        }

        public ICommand OnImageInput { get; set; }

        public CreateOfferViewModel(IApplicationContext context) : base(context)
        {
            Image = new BitmapImage(new Uri(@"pack://application:,,,/EmptyImage/EmptyImage.png", UriKind.Absolute));
            OnImageInput = new RelayCommand<BitmapImage>(ImageInput);
        }

        private void ImageInput(BitmapImage newImage)
        {
            Image = newImage;
        }
    }
}
