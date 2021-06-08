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
    /// Interaction logic for ClientSeatingLayoutModal.xaml
    /// </summary>
    public partial class ClientSeatingLayoutModal : Window, IModalWindow
    {
        public ClientSeatingLayoutModal()
        {
            InitializeComponent();
        }
    }
}
