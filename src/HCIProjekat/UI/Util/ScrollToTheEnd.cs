﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace UI.Util
{
    public class ScrollToTheEnd
    {
        public static bool GetAutoScroll(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoScrollProperty);
        }

        public static void SetAutoScroll(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoScrollProperty, value);
        }

        public static readonly DependencyProperty AutoScrollProperty =
            DependencyProperty.RegisterAttached("AutoScroll", typeof(bool), typeof(ScrollToTheEnd), new PropertyMetadata(false, AutoScrollPropertyChanged));

        private static void AutoScrollPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var scrollViewer = d as ScrollViewer;
            //MessageBox.Show($"TREBA SKROL DO KRAJA!");

            if (scrollViewer != null && (bool)e.NewValue)
            {
                scrollViewer.ScrollToBottom();
            }
        }
    }
}
