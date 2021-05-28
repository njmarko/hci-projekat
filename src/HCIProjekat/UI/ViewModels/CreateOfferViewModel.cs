using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using UI.Context;

namespace UI.ViewModels
{
    public class CreateOfferViewModel : ViewModelBase
    {
        private string _image;
        public string Image
        {
            get { return _image; }
            set { _image = value; OnPropertyChanged(nameof(Image)); }
        }

        public CreateOfferViewModel(IApplicationContext context) : base(context)
        {
            this.Image = "/Images/EmptyImage.png";
        }
    }
}
