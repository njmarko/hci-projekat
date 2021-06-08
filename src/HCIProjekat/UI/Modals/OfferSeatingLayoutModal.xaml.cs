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
    /// Interaction logic for OfferSeatingLayoutModal.xaml
    /// </summary>
    public partial class OfferSeatingLayoutModal : Window, IModalWindow
    {
        private readonly double TABLE_RADIUS = 40;
        private readonly double CHAIR_RADIUS = 10;
        private readonly double TABLE_DISTANCE_TRESHOLD = 100;
        private CreateOfferSeatingLayoutViewModel _vm;

        public string CurrentItem { get; set; } = null;
        public ChairIcon SelectedChair { get; set; } = null;
        public TableIcon SelectedTable { get; set; } = null;

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

        private void OnSelectedDrop(DragEventArgs e)
        {
            var context = (SelectedTable != null ? SelectedTable.DataContext : SelectedChair.DataContext) as ILayoutItem;
            var position = e.GetPosition(_mainCanvas);
            var radius = context.Radius;
            var xOffset = position.X - radius;
            var yOffset = position.Y - radius;

            if (SelectedTable != null)
            {
                _vm.UpdateTable(context.X, context.Y, position.X, position.Y);
            }
            else
            {
                _vm.UpdateChair(context.X, context.Y, position.X, position.Y);
            }

            context.X = position.X;
            context.Y = position.Y;

            UserControl item = SelectedTable != null ? SelectedTable : SelectedChair;
            Canvas.SetLeft(item, xOffset);
            Canvas.SetTop(item, yOffset);

            _mainCanvas.Children.RemoveRange(1, _mainCanvas.Children.Count - 1);
            DrawLayout();
        }

        private void OnItemDrop(object sender, DragEventArgs e)
        {
            if (CurrentItem == null && SelectedChair == null && SelectedTable == null)
            {
                return;
            }

            if (SelectedTable != null || SelectedChair != null)
            {
                OnSelectedDrop(e);
                return;
            }

            var position = e.GetPosition(_mainCanvas);
            var radius = CurrentItem == "Table" ? TABLE_RADIUS : CHAIR_RADIUS;
            var xOffset = position.X - radius;
            var yOffset = position.Y - radius;
            ILayoutItem saved;
            if (CurrentItem == "Table")
            {
                saved = _vm.AddTable(position.X, position.Y);
            }
            else
            {
                saved = _vm.AddChair(position.X, position.Y);
            }
            var label = saved.Label;
            UserControl item = CurrentItem switch
            {
                "Table" => new TableIcon { Width = 2 * radius, Height = 2 * radius, AllowDrop = false, ToolTip = label, DataContext = saved, SeatingLayoutModal = this },
                "Chair" => new ChairIcon { Width = 2 * radius, Height = 2 * radius, AllowDrop = false, ToolTip = label, DataContext = saved, SeatingLayoutModal = this },
                _ => throw new Exception("Invalid item type"),
            };
            Canvas.SetLeft(item, xOffset);
            Canvas.SetTop(item, yOffset);
            _mainCanvas.Children.Add(item);
        }

        private void OnChairDrag(object sender, DragEventArgs e)
        {
            if (CurrentItem != "Chair" && SelectedChair == null)
            {
                return;
            }
            var position = e.GetPosition(_mainCanvas);
            var closestTable = _vm.ClosestTable(position.X, position.Y);
            if (closestTable == null || _vm.Distance(closestTable, position.X, position.Y) > TABLE_DISTANCE_TRESHOLD)
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
            }
        }

        private void InitItemDrop(object sender, MouseButtonEventArgs e)
        {
            SelectedChair = null;
            SelectedTable = null;

            var item = sender as ListViewItem;
            CurrentItem = item.Content.ToString();
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
            var t = new TableIcon { Width = 2 * TABLE_RADIUS, Height = 2 * TABLE_RADIUS, AllowDrop = false, ToolTip = table.Label, DataContext = table, SeatingLayoutModal = this };
            Canvas.SetLeft(t, table.X - TABLE_RADIUS);
            Canvas.SetTop(t, table.Y - TABLE_RADIUS);
            _mainCanvas.Children.Add(t);
        }

        private void DrawChair(Chair chair)
        {
            var c = new ChairIcon { Width = 2 * CHAIR_RADIUS, Height = 2 * CHAIR_RADIUS, AllowDrop = false, ToolTip = chair.Label, DataContext = chair, SeatingLayoutModal = this };
            Canvas.SetLeft(c, chair.X - CHAIR_RADIUS);
            Canvas.SetTop(c, chair.Y - CHAIR_RADIUS);
            _mainCanvas.Children.Add(c);
        }
    }
}
