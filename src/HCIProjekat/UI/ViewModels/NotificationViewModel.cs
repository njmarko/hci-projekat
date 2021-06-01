using Domain.Entities;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Context;

namespace UI.ViewModels
{
    public class NotificationModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string TimeStamp { get; set; }
        public bool IsRead { get; set; }
        public bool IsSeen { get; set; }
        public bool IsNew { get; set; }
    }

    public class NotificationViewModel : ViewModelBase
    {
        private readonly INotificationService _notificationService;

        private bool? _markedAsSeen = null;
        private int _notificationCount = 0;
        public int NotificationCount
        {
            get { return _notificationCount; }
            set { _notificationCount = value; OnPropertyChanged(nameof(NotificationCount)); }
        }

        private bool _didOpenNotifications = false;
        public bool DidOpenNotifications
        {
            get { return _didOpenNotifications; }
            set
            {
                _didOpenNotifications = value;
                if (_didOpenNotifications && NotificationCount > 0 && !_markedAsSeen.HasValue)
                {
                    MarkAllAsSeen();
                }
                else if (!_didOpenNotifications && (_markedAsSeen.HasValue && _markedAsSeen.Value))
                {
                    UpdateNotifications(Context.Store.CurrentUser);
                    _markedAsSeen = false;
                }
                OnPropertyChanged(nameof(DidOpenNotifications));
            }
        }

        public ObservableCollection<NotificationModel> Notifications { get; private set; } = new ObservableCollection<NotificationModel>();

        public NotificationViewModel(IApplicationContext context, INotificationService notificationService) : base(context)
        {
            _notificationService = notificationService;

            context.Store.CurrentUserChanged += UpdateNotifications;
        }

        private void UpdateNotifications(User currentUser)
        {
            if (currentUser != null)
            {
                Notifications.Clear();
                var notifications = _notificationService.Read(currentUser.Id);
                InsertNotifications(notifications);
                NotificationCount = notifications.Count;
            }
        }

        private void InsertNotifications(IEnumerable<Notification> notifications)
        {
            foreach (var notification in notifications)
            {
                InsertNotification(notification);
            }
        }

        private void InsertNotification(Notification notification)
        {
            var model = new NotificationModel
            {
                Id = notification.Id,
                Message = notification.Message,
                TimeStamp = notification.TimeStamp.ToString("dd.MM.yyyy. HH:mm"),
                IsSeen = notification.Seen,
                IsRead = notification.Read,
                IsNew = !notification.Seen,
            };
            Notifications.Add(model);
        }

        private void MarkAllAsSeen()
        {
            _notificationService.Seen(Context.Store.CurrentUser.Id);
            _markedAsSeen = true;
            //NotificationCount = 0;
        }
    }
}
