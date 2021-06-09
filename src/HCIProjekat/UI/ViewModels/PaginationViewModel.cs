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
        public string ToolTip { get; set; }
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

            PageModels.Add(new PageModel { Text = "<<", Page = 0, IsEnabled = Page != 0, ToolTip = "Go to the first page" });
            PageModels.Add(new PageModel { Text = $"<", Page = Page - 1, IsEnabled = Page != 0, ToolTip = "Go to the page before the current" });
            var currentPage = PageCount == 0 ? 0 : Page + 1;
            PageModels.Add(new PageModel { Text = $"{currentPage} / {PageCount}", Page = page, IsChecked = true, ToolTip = "Current page" } );
            PageModels.Add(new PageModel { Text = $">", Page = Page + 1, IsEnabled = Page != PageCount - 1 && PageCount != 0, ToolTip = "Go to the page after the current" });
            PageModels.Add(new PageModel { Text = ">>", Page = -1, IsEnabled = Page != PageCount - 1 && PageCount != 0, ToolTip = "Go to the last page" });
        }
    }
}
