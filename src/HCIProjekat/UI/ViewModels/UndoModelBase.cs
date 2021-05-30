using Domain.Services;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToastNotifications.Messages;
using UI.Commands;
using UI.Context;
using UI.Services.Interfaces;

namespace UI.ViewModels
{
    public abstract class UndoModelBase<T> : PagingViewModelBase
    {
        public ICommand Undo { get; private set; }
        private Stack<T> UndoItems { get; set; } = new Stack<T>();
        private readonly ICRUDService<T> _service;
        private readonly IModalService _modalService;
        public bool CanUndo => UndoItems.Count > 0;

        protected UndoModelBase(IApplicationContext context, ICRUDService<T> service, IModalService modalService) : base(context) 
        {
            _service = service;
            _modalService = modalService;
            Undo = new UndoCommand<T>(this, UndoAction);
        }

        public void AddItem(T item)
        {
            var clonedItem = CloningService.Clone<T>(item);
            UndoItems.Push(clonedItem);
            OnPropertyChanged(nameof(CanUndo));
        }

        private void UndoAction()
        {
            var ok = _modalService.ShowConfirmationDialog("Are you sure you want to undo last action?");
            if (ok)
            {
                var item = UndoItems.Pop();
                _service.Update(item);
                Context.Notifier.ShowInformation("Action successfully reverted");
                OnPropertyChanged(nameof(CanUndo));
                UpdatePage(0);
            }
        }

    }
}
