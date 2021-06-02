using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.ViewModels;

namespace UI.Commands
{
    public class RedoCommand<T> : ICommand where T : BaseEntity
    {
        public event EventHandler CanExecuteChanged;
        private UndoModelBase<T> _model;
        private Action _action;

        public RedoCommand(UndoModelBase<T> model, Action action)
        {
            _model = model;
            _action = action;
            _model.PropertyChanged += _undoVm_PropertyChanged;
        }

        private void _undoVm_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(UndoModelBase<T>.CanRedo))
            {
                CanExecuteChanged?.Invoke(sender, e);
            }
        }

        public bool CanExecute(object parameter)
        {
            return _model.CanRedo;
        }

        public void Execute(object parameter)
        {
            _action();
        }
    }
}
