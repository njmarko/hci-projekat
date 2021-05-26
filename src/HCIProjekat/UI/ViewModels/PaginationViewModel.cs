using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.ViewModels
{
    public class PageModel
    {
        public int Page { get; set; }
        public bool IsChecked { get; set; }
    }

    public class PaginationViewModel
    {
        public ObservableCollection<PageModel> PageModels { get; private set; } = new ObservableCollection<PageModel>();

        public int Page { get; set; }
        public int TotalElements { get; set; }
        public int PerPage { get; set; }
        public int PageCount { get; set; }

        public PaginationViewModel(int page, int totalElements, int perPage, int pageCount)
        {
            Page = page;
            TotalElements = totalElements;
            PerPage = perPage;
            PageCount = pageCount;
            for (int i = 0; i < PageCount; i++)
            {
                PageModels.Add(new PageModel { Page = i, IsChecked = page == i });
            }
        }
    }
}
