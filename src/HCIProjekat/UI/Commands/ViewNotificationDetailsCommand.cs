using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.ViewModels;

namespace UI.Commands
{
    public class ViewNotificationDetailsCommand : ICommand
    {
        private readonly NotificationViewModel _notificationVm;
        private readonly NotificationModel _notificationModel;
        private readonly INotificationService _notificationService;

        public event EventHandler CanExecuteChanged;

        public ViewNotificationDetailsCommand(NotificationViewModel notificationVm, NotificationModel notificationModel, INotificationService notificationService)
        {
            _notificationVm = notificationVm;
            _notificationModel = notificationModel;
            _notificationService = notificationService;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var notification = _notificationService.ReadNotification(_notificationModel.Id);
            _notificationVm.OnNotificationRead();
            if (notification.TaskId != -1)
            {
                _notificationVm.Context.Router.Push($"TaskDetails?taskId={notification.TaskId}");
            }
            else if (notification.RequestId != -1)
            {
                _notificationVm.Context.Router.Push($"RequestDetails?requestId={notification.RequestId}");
            }
        }
    }
}
