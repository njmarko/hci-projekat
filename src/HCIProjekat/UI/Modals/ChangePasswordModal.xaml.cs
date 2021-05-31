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

namespace UI.Modals
{
    /// <summary>
    /// Interaction logic for ChangePasswordModal.xaml
    /// </summary>
    public partial class ChangePasswordModal : Window, IModalWindow
    {
        public ChangePasswordModal()
        {
            InitializeComponent();
        }

        private void OldPasswordPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).OldPassword = ((PasswordBox)sender).Password;
            }
        }

        private void NewPasswordPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).NewPassword = ((PasswordBox)sender).Password;
            }
        }

        private void ConfirmPasswordPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).ConfirmPassword = ((PasswordBox)sender).Password;
            }
        }
    }
}
