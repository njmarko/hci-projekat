using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Modals.Interfaces
{
    public interface IModalWindow
    {
        object DataContext { get; set; }
        bool? ShowDialog();
    }
}
