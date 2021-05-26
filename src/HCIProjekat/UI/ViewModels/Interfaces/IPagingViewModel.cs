using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UI.ViewModels.Interfaces
{
    public interface IPagingViewModel
    {
        public int Rows { get; }
        public int Columns { get; }
        public PaginationViewModel PaginationViewModel { get; }

        public ICommand UpdatePageCommand { get; }
        public void UpdatePage(int pageNumber);
    }
}
