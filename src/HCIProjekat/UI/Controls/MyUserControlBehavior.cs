using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace UI.Controls
{
    // Way to change window title from user control
    // found at: https://stackoverflow.com/a/2176695/13066849
    public static class MyUserControlBehavior
    {
        public static readonly DependencyProperty WindowTitleProperty = DependencyProperty.RegisterAttached("WindowTitleProperty",
                     typeof(string), typeof(UserControl),
                     new FrameworkPropertyMetadata(null, WindowTitlePropertyChanged));

        public static string GetWindowTitle(DependencyObject element)
        {
            return (string)element.GetValue(WindowTitleProperty);
        }

        public static void SetWindowTitle(DependencyObject element, string value)
        {
            element.SetValue(WindowTitleProperty, value);
        }

        private static void WindowTitlePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Application.Current.MainWindow.Title = (string)e.NewValue;
        }
    }
}
