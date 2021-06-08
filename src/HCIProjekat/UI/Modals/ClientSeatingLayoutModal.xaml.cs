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
        private readonly double GUEST_RADIUS = 15;
        private readonly double CHAIR_DISTANCE_THRESHOLD = 50;

        private GuestModel _currentGuest;
        private GuestIcon _currentGuestIcon;
        private ClientSeatingLayoutViewModel _vm;

        public ClientSeatingLayoutModal()
        {
            InitializeComponent();
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
            if (_currentGuest == null && _currentGuestIcon == null)
            {
                return;
            }
            var position = e.GetPosition(_mainCanvas);
            var closestChair = _vm.ClosestChair(position.X, position.Y);
            GuestIcon guest = null;
            if (_currentGuest != null)
            {
                var guestModel = _vm.DropGuest(_currentGuest.Id, position.X, position.Y);
                guest = new GuestIcon { Width = 2 * GUEST_RADIUS, Height = 2 * GUEST_RADIUS, AllowDrop = false, ToolTip = $"{_currentGuest.Name}", DataContext = guestModel };
                guest.PreviewMouseDown += Guest_PreviewMouseDown;
                _currentGuest = null;
            }
            else
            {
                _mainCanvas.Children.Remove(_currentGuestIcon);
                var guestModel = _vm.DropGuest((_currentGuestIcon.DataContext as GuestModel).Id, position.X, position.Y);
                guest = new GuestIcon { Width = 2 * GUEST_RADIUS, Height = 2 * GUEST_RADIUS, AllowDrop = false, ToolTip = $"{guestModel.Name}", DataContext = guestModel };
                guest.PreviewMouseDown += Guest_PreviewMouseDown;
                _currentGuestIcon = null;
            }
            Canvas.SetLeft(guest, closestChair.X - GUEST_RADIUS);
            Canvas.SetTop(guest, closestChair.Y - GUEST_RADIUS);
            _mainCanvas.Children.Add(guest);
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
            var id = -1;
            if (_currentGuest != null)
            {
                id = _currentGuest.Id;
            } else if (_currentGuestIcon != null)
            {
                id = (_currentGuestIcon.DataContext as GuestModel).Id;
            }
            var chair = _vm.ClosestChair(position.X, position.Y, id);
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
                if (e.Data.GetDataPresent(DataFormats.FileDrop, true))
                {
                    var filenames = e.Data.GetData(DataFormats.FileDrop) as string[];
                    if (filenames.Length > 1)
                    {
                        e.Effects = DragDropEffects.None;
                        e.Handled = true;
                    } 
                    else
                    {
                        var filename = filenames[0];
                        if (System.IO.Path.GetExtension(filename).ToLowerInvariant() != ".txt")
                        {
                            e.Effects = DragDropEffects.None;
                            e.Handled = true;
                        }
                    }
                    return;
                }
                e.Effects = DragDropEffects.None;
                e.Handled = true;
                return;
            }
        }

        private void OnListItemDrop(object sender, DragEventArgs e)
        {
            if (_currentGuestIcon == null)
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop, true))
                {
                    var filename = (e.Data.GetData(DataFormats.FileDrop) as string[])[0];
                    _vm.InsertGuestsFromFile(filename);
                }
                return;
            }
            _mainCanvas.Children.Remove(_currentGuestIcon);
            _vm.FreeChair((_currentGuestIcon.DataContext as GuestModel).Id);
            _currentGuestIcon = null;
        }
    }
}
