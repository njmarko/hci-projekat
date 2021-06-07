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
        }

        private void EventPlannerTaskOfferCard_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var fe = sender as FrameworkElement;
            _currentOffer = fe.DataContext as EventPlannerTaskOfferCardModel;
            DragDrop.DoDragDrop(fe, "nesto", DragDropEffects.Move);
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
    }
}
