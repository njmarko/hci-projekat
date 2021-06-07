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
using UI.Modals.Interfaces;
using UI.ViewModels;

namespace UI.Modals
{
    /// <summary>
    /// Interaction logic for OfferSeatingLayoutModal.xaml
    /// </summary>
    public partial class OfferSeatingLayoutModal : Window, IModalWindow
    {
        private readonly double TABLE_RADIUS = 40;
        private readonly double CHAIR_RADIUS = 10;
        private readonly double TABLE_DISTANCE_TRESHOLD = 50;

        private string _currentItem = null;
        private CreateOfferSeatingLayoutViewModel _vm;

        public OfferSeatingLayoutModal()
        {
            InitializeComponent();
        }

        private void OnCanvasResized(object sender, SizeChangedEventArgs e)
        {
            _mainContainter.Width = e.NewSize.Width;
            _mainContainter.Height = e.NewSize.Height;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _vm = DataContext as CreateOfferSeatingLayoutViewModel;
            DrawLayout();
        }

        private void OnItemDrop(object sender, DragEventArgs e)
        {
            if (_currentItem == null)
            {
                return;
            }
            var position = e.GetPosition(_mainCanvas);
            var radius = _currentItem == "Table" ? TABLE_RADIUS : CHAIR_RADIUS;
            double xOffset = position.X - radius;
            double yOffset = position.Y - radius;
            var item = _currentItem switch
            {
                "Table" => new Ellipse { Fill = Brushes.Blue, Width = 2 * radius, Height = 2 * radius, AllowDrop = false },
                "Chair" => new Ellipse { Fill = Brushes.Red, Width = 2 * radius, Height = 2 * radius, AllowDrop = false },
                _ => throw new Exception("Invalid item type"),
            };
            Canvas.SetLeft(item, xOffset);
            Canvas.SetTop(item, yOffset);
            _mainCanvas.Children.Add(item);
            _currentItem = null;
        }

        private void OnChairDrag(object sender, DragEventArgs e)
        {
            if (_currentItem != "Chair")
            {
                return;
            }
            var position = e.GetPosition(_mainCanvas);
            double xOffset = position.X - CHAIR_RADIUS;
            double yOffset = position.Y - CHAIR_RADIUS;
            var closestTable = _vm.ClosestTable(xOffset, yOffset);
            if (closestTable == null || _vm.Distance(closestTable, xOffset, yOffset) > TABLE_DISTANCE_TRESHOLD)
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
            }
        }

        private void InitItemDrop(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            _currentItem = item.Content.ToString();
            DragDrop.DoDragDrop(item, "nesto", DragDropEffects.Copy);
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

        private void DrawTable(Domain.Entities.Table table)
        {

        }

        private void DrawChair(Chair chair)
        {

        }
    }
}
