using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UI.ViewModels;

namespace UI.Views
{
    /// <summary>
    /// Interaction logic for CreateOfferView.xaml
    /// </summary>
    public partial class CreateOfferView : UserControl
    {
        public CreateOfferView()
        {
            InitializeComponent();
        }

        private void InputImage(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (dialog.ShowDialog() == true)
            {
                var fileUri = new Uri(dialog.FileName);
                var vm = DataContext as CreateOfferViewModel;
                vm.OnImageInput?.Execute(new BitmapImage(fileUri));
            }
        }
    }
}
