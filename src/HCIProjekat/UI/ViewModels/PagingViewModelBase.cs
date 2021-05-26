using Domain.Entities;
using Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.Commands;
using UI.Context;
using UI.ViewModels.Interfaces;

namespace UI.ViewModels
{
    public abstract class PagingViewModelBase : ViewModelBase, IPagingViewModel
    {
        public int Rows { get; protected set; } = 2;
        public int Columns { get; protected set; } = 3;
        public int Size => Rows * Columns;

        public ICommand UpdatePageCommand { get; protected set; }

        public PaginationViewModel PaginationViewModel { get; protected set; }

        public PagingViewModelBase(IApplicationContext context) : base(context)
        {
            UpdatePageCommand = new UpdatePageCommand(this);
        }

        public abstract void UpdatePage(int pageNumber);

        protected void OnPageFetched<TEntity>(Page<TEntity> page) where TEntity : BaseEntity
        {
            PaginationViewModel = new PaginationViewModel(page.PageNumber, page.TotalElements, page.Size, page.PageCount);
            OnPropertyChanged(nameof(PaginationViewModel));
        }
    }
}
