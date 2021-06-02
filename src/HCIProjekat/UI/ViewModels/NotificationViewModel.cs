using Domain.Entities;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.Commands;
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

        public ICommand ViewDetails { get; set; }
    }

    public class NotificationViewModel : ViewModelBase
    {
        private readonly INotificationService _notificationService;

        private bool _isLoaded = true;
        private bool _markedAsSeen = false;
        private int _notificationCount = 0;
        public int NotificationCount
        {
            get { return _notificationCount; }
            set { _notificationCount = value; OnPropertyChanged(nameof(NotificationCount)); }
        }

        public ICommand ViewNotifications { get; private set; }
        public ObservableCollection<NotificationModel> Notifications { get; private set; } = new ObservableCollection<NotificationModel>();

        public NotificationViewModel(IApplicationContext context, INotificationService notificationService) : base(context)
        {
            _notificationService = notificationService;

            context.Store.CurrentUserChanged += UpdateNotifications;
        }

        public void Update()
        {
            if (_isLoaded)
            {
                if (!_markedAsSeen)
                {
                    MarkAllAsSeen();
                    UpdateNotifications(Context.Store.CurrentUser);
                    _markedAsSeen = true;
                }
            }
            else
            {
                _isLoaded = false;
            }
        }

        public void OnNotificationRead()
        {
            UpdateNotifications(Context.Store.CurrentUser);
        }

        private void UpdateNotifications(User currentUser)
        {
            if (currentUser != null)
            {
                Notifications.Clear();
                _isLoaded = true;
                Refetch(currentUser);
                _markedAsSeen = false;
            }
        }

        public void Refetch(User currentUser)
        {
            var notifications = _notificationService.Read(currentUser.Id);
            InsertNotifications(notifications);
            NotificationCount = notifications.Count(n => !n.Read);
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
                IsSeen = notification.Seen && !notification.Read,
                IsRead = notification.Read && notification.Seen,
                IsNew = !notification.Seen,
            };
            model.ViewDetails = new ViewNotificationDetailsCommand(this, model, _notificationService);
            Notifications.Add(model);
        }

        private void MarkAllAsSeen()
        {
            _notificationService.Seen(Context.Store.CurrentUser.Id);
        }
    }
}
