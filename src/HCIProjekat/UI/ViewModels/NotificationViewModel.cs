﻿using Domain.Entities;
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
    }

    public class NotificationViewModel : ViewModelBase
    {
        private readonly INotificationService _notificationService;

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
                if (_didOpenNotifications && NotificationCount > 0)
                {
                    MarkAllAsSeen();
                }
                OnPropertyChanged(nameof(DidOpenNotifications));
            }
        }

        public ObservableCollection<Notification> Notifications { get; private set; } = new ObservableCollection<Notification>();

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
                Message = notification.Message
            };
            Notifications.Add(notification);
        }

        private void MarkAllAsSeen()
        {
            _notificationService.Seen(Context.Store.CurrentUser.Id);
            //NotificationCount = 0;
        }
    }
}
