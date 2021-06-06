using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UI.Modals.Interfaces;
using UI.Util;

namespace UI.Modals
{
    /// <summary>
    /// Interaction logic for OfferModal.xaml
    /// </summary>
    public partial class OfferModal : Window, IModalWindow
    {
        public OfferModal()
        {
            InitializeComponent();
        }

        private void OnImageDrop(object sender, DragEventArgs e)
        {
            var imagePath = (e.Data.GetData(DataFormats.FileDrop, true) as string[])[0];
            var newImage = ImageUtil.ReadFromFile(imagePath);
            var vm = DataContext as dynamic;
            vm.ImageInBytes = newImage;
            vm.Image = ImageUtil.ConvertToImage(newImage);
        }

        private void OnImageDrag(object sender, DragEventArgs e)
        {
            bool dropEnabled = true;
            if (e.Data.GetDataPresent(DataFormats.FileDrop, true))
            {
                string[] filenames = e.Data.GetData(DataFormats.FileDrop, true) as string[];
                if (filenames.Length > 1)
                {
                    dropEnabled = false;
                }
                else
                {
                    var imagePath = filenames[0];
                    // TODO: Dodati podrsku za sve moguce tipove za sliku
                    if ((System.IO.Path.GetExtension(imagePath).ToLowerInvariant() != ".jpg") && (System.IO.Path.GetExtension(imagePath).ToLowerInvariant() != ".png"))
                    {
                        dropEnabled = false;
                    }
                }
            }
            else
            {
                dropEnabled = false;
            }

            if (!dropEnabled)
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
            }
        }
    }
}
