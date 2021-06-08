using Domain.Entities;
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
    /// Interaction logic for TableIcon.xaml
    /// </summary>
    public partial class TableIcon : UserControl
    {
        public OfferSeatingLayoutModal SeatingLayoutModal { get; set; }

        public TableIcon()
        {
            InitializeComponent();
        }

        private void TableSelected(object sender, MouseButtonEventArgs e)
        {
            SeatingLayoutModal.SelectedChair = null;
            SeatingLayoutModal.SelectedTable = this;
            SeatingLayoutModal.CurrentItem = null;

            var vm = SeatingLayoutModal.DataContext as CreateOfferSeatingLayoutViewModel;
            var thisVm = DataContext as ILayoutItem;

            if (vm.ChairsPresentInTable(thisVm.X, thisVm.Y))
            {
                return;
            }

            var fe = sender as FrameworkElement;
            DragDrop.DoDragDrop(fe, "nesto", DragDropEffects.Move);
        }
    }
}
