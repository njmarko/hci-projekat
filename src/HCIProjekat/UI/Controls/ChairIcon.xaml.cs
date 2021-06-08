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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UI.Modals;
using UI.ViewModels;

namespace UI.Controls
{
    /// <summary>
    /// Interaction logic for ChairIcon.xaml
    /// </summary>
    public partial class ChairIcon : UserControl
    {
        public OfferSeatingLayoutModal SeatingLayoutModal { get; set; }

        public ChairIcon()
        {
            InitializeComponent();
        }

        private void ChairSelected(object sender, MouseButtonEventArgs e)
        {
            SeatingLayoutModal.SelectedChair = this;
            SeatingLayoutModal.SelectedTable = null;
            SeatingLayoutModal.CurrentItem = null;

            var fe = sender as FrameworkElement;
            DragDrop.DoDragDrop(fe, "nesto", DragDropEffects.Move);
        }
    }
}
