﻿using Domain.Entities;
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
            var c = new ChairIcon { Width = 2 * CHAIR_RADIUS, Height = 2 * CHAIR_RADIUS, AllowDrop = false, ToolTip = chair.Label, DataContext = chair };
            Canvas.SetLeft(c, chair.X - CHAIR_RADIUS);
            Canvas.SetTop(c, chair.Y - CHAIR_RADIUS);
            _mainCanvas.Children.Add(c);
        }
    }
}
