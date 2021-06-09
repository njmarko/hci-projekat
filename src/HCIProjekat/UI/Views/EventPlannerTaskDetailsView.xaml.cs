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
    /// Interaction logic for EventPlannerTaskDetailsView.xaml
    /// </summary>
    public partial class EventPlannerTaskDetailsView : UserControl
    {
        private EventPlannerTaskOfferCardModel _currentOffer = null;

        public EventPlannerTaskDetailsView()
        {
            InitializeComponent();
            Focusable = true;
            Loaded += (s, e) => Keyboard.Focus(this);
        }

        private void ShowAvailableOffers(object sender, DragEventArgs e)
        {
            if (e.Effects.HasFlag(DragDropEffects.Move))
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
                return;
            }

            var vm = DataContext as EventPlannerTaskDetailsViewModel;
            vm.TabSelectedIndex = 1;
        }

        private void ShowAddedOffers(object sender, DragEventArgs e)
        {
            if (e.Effects.HasFlag(DragDropEffects.Copy))
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
                return;
            }

            var vm = DataContext as EventPlannerTaskDetailsViewModel;
            vm.TabSelectedIndex = 0;
        }

        private void AvailableSelected(object sender, MouseButtonEventArgs e)
        {
            var vm = DataContext as EventPlannerTaskDetailsViewModel;
            vm.AddedOffersVm.AddedDrop = true;
            vm.AvailableOffersVm.AvailableDrop = false;

            var fe = sender as FrameworkElement;
            _currentOffer = fe.DataContext as EventPlannerTaskOfferCardModel;
            DragDrop.DoDragDrop(fe, "nesto", DragDropEffects.Move);
        }

        private void AddedSelected(object sender, MouseButtonEventArgs e)
        {
            var vm = DataContext as EventPlannerTaskDetailsViewModel;
            vm.AddedOffersVm.AddedDrop = false;
            vm.AvailableOffersVm.AvailableDrop = true;

            var fe = sender as FrameworkElement;
            _currentOffer = fe.DataContext as EventPlannerTaskOfferCardModel;
            DragDrop.DoDragDrop(fe, "nesto", DragDropEffects.Copy);
        }

        private void OnOfferAdded(object sender, DragEventArgs e)
        {
            if (_currentOffer == null)
            {
                return;
            }
            _currentOffer.ButtonAction.Execute(null);
            _currentOffer = null;
        }

        private void OnOfferRemoved(object sender, DragEventArgs e)
        {
            if (_currentOffer == null)
            {
                return;
            }
            _currentOffer.ButtonAction.Execute(null);
            _currentOffer = null; 
        }

    }
}
