﻿using Domain.Entities;
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
    public abstract class UndoModelBase<T> : PagingViewModelBase where T : BaseEntity
    {
        private readonly Stack<T> _undoItems = new();
        private readonly Stack<T> _redoItems = new();
        private readonly ICRUDService<T> _service;
        private readonly IModalService _modalService;

        public bool CanUndo => _undoItems.Count > 0;
        public bool CanRedo => _redoItems.Count > 0;

        public override ICommand Undo { get; set; }
        public override ICommand Redo { get; set; }

        protected UndoModelBase(IApplicationContext context, ICRUDService<T> service, IModalService modalService) : base(context) 
        {
            _service = service;
            _modalService = modalService;
            Undo = new UndoCommand<T>(this, UndoAction);
            Redo = new RedoCommand<T>(this, RedoAction);
        }

        public void AddItem(T item)
        {
            var clonedItem = item.Clone();
            _undoItems.Push(clonedItem);
            _redoItems.Clear();
            OnPropertyChanged(nameof(CanUndo));
            OnPropertyChanged(nameof(CanRedo));
        }

        private void UndoAction()
        {
            var item = _undoItems.Pop();
            var currentItem = _service.Get(item.Id);
            _redoItems.Push(currentItem.Clone());
            _service.Update(item);
            Context.Notifier.ShowInformation("Action successfully reverted");
            OnPropertyChanged(nameof(CanUndo));
            OnPropertyChanged(nameof(CanRedo));
            UpdatePage(0);
        }

        private void RedoAction()
        {
            var item = _redoItems.Pop();
            var currentItem = _service.Get(item.Id);
            _undoItems.Push(currentItem.Clone());
            _service.Update(item);
            Context.Notifier.ShowInformation("Action successfully reverted");
            OnPropertyChanged(nameof(CanUndo));
            OnPropertyChanged(nameof(CanRedo));
            UpdatePage(0);
        }

    }
}
