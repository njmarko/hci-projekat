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
        public string Text { get; set; }
        public int Page { get; set; }
        public bool IsChecked { get; set; } = false;
        public bool IsEnabled { get; set; } = true;
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

            PageModels.Add(new PageModel { Text = "<<", Page = 0, IsEnabled = Page != 0 });
            PageModels.Add(new PageModel { Text = $"<", Page = Page - 1, IsEnabled = Page != 0 });
            PageModels.Add(new PageModel { Text = $"{Page + 1} / {PageCount}", Page = page, IsChecked = true } );
            PageModels.Add(new PageModel { Text = $">", Page = Page + 1, IsEnabled = Page != PageCount - 1 });
            PageModels.Add(new PageModel { Text = ">>", Page = -1, IsEnabled = Page != PageCount - 1 });
        }
    }
}
