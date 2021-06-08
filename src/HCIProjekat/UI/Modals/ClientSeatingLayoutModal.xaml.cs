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
using System.Windows.Shapes;
using UI.Controls;
using UI.Modals.Interfaces;
using UI.ViewModels;

namespace UI.Modals
{
    /// <summary>
    /// Interaction logic for ClientSeatingLayoutModal.xaml
    /// </summary>
    public partial class ClientSeatingLayoutModal : Window, IModalWindow
    {
        private readonly double TABLE_RADIUS = 40;
        private readonly double CHAIR_RADIUS = 10;
        private readonly double CHAIR_DISTANCE_THRESHOLD = 50;

        private GuestModel _currentGuest;
        private GuestIcon _currentGuestIcon;
        private ClientSeatingLayoutViewModel _vm;

        public ClientSeatingLayoutModal()
        {
            InitializeComponent();
        }

        private void OnCanvasResized(object sender, SizeChangedEventArgs e)
        {
            _mainContainter.Width = e.NewSize.Width;
            _mainContainter.Height = e.NewSize.Height;
        }

        private void DrawLayout()
        {
            if (_vm.SeatingLayout != null)
            {
                foreach (var table in _vm.SeatingLayout.Tables)
                {
                    DrawTable(table);
                    foreach (var chair in table.Chairs)
                    {
                        DrawChair(chair);
                    }
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _vm = DataContext as ClientSeatingLayoutViewModel;
            DrawLayout();
        }

        private void DrawTable(Domain.Entities.Table table)
        {
            var t = new TableIcon { Width = 2 * TABLE_RADIUS, Height = 2 * TABLE_RADIUS, AllowDrop = false, ToolTip = table.Label, DataContext = table };
            Canvas.SetLeft(t, table.X - TABLE_RADIUS);
            Canvas.SetTop(t, table.Y - TABLE_RADIUS);
            _mainCanvas.Children.Add(t);
        }

        private void DrawChair(Chair chair)
        {
            var c = new ChairIcon { Width = 2 * CHAIR_RADIUS, Height = 2 * CHAIR_RADIUS, AllowDrop = true, ToolTip = chair.Label, DataContext = chair };
            Canvas.SetLeft(c, chair.X - CHAIR_RADIUS);
            Canvas.SetTop(c, chair.Y - CHAIR_RADIUS);
            _mainCanvas.Children.Add(c);
        }

        private void InitGuestDrag(object sender, MouseButtonEventArgs e)
        {
            var frameworkElement = sender as FrameworkElement;
            _currentGuest = frameworkElement.DataContext as GuestModel;
            DragDrop.DoDragDrop(frameworkElement, _currentGuest.Name, DragDropEffects.Copy);
        }

        private void OnGuestDrop(object sender, DragEventArgs e)
        {
            if (_currentGuest == null)
            {
                return;
            }
            var position = e.GetPosition(_mainCanvas);
            var closestChair = _vm.ClosestChair(position.X, position.Y);
            var guestModel = _vm.DropGuest(_currentGuest.Id, position.X, position.Y);
            var guest = new GuestIcon { Width = 25, Height = 25, AllowDrop = false, ToolTip = $"{_currentGuest.Name}", DataContext = guestModel };
            guest.PreviewMouseDown += Guest_PreviewMouseDown;
            Canvas.SetLeft(guest, closestChair.X - 12.5);
            Canvas.SetTop(guest, closestChair.Y - 12.5);
            _mainCanvas.Children.Add(guest);
            _currentGuest = null;
        }

        private void Guest_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            _currentGuestIcon = sender as GuestIcon;
            var name = (_currentGuestIcon.DataContext as GuestModel).Name;
            DragDrop.DoDragDrop(_currentGuestIcon, name, DragDropEffects.Copy);
        }

        private void OnGuestDrag(object sender, DragEventArgs e)
        {
            var position = e.GetPosition(_mainCanvas);
            if (_vm.SeatingLayout == null)
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
                return;
            }
            var chair = _vm.ClosestChair(position.X, position.Y);
            if (chair == null || _vm.Distance(chair, position.X, position.Y) > CHAIR_DISTANCE_THRESHOLD)
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
            }
        }

        private void OnItemDragList(object sender, DragEventArgs e)
        {
            if (_currentGuestIcon == null)
            {
                // TODO: Ubaci proveru ako je fajl
                e.Effects = DragDropEffects.None;
                e.Handled = true;
                return;
            }
        }

        private void OnListItemDrop(object sender, DragEventArgs e)
        {
            if (_currentGuestIcon == null)
            {
                return;
            }
            _mainCanvas.Children.Remove(_currentGuestIcon);
            _vm.FreeChair((_currentGuestIcon.DataContext as GuestModel).Id);
            _currentGuestIcon = null;
        }
    }
}
