using Domain.Entities;
using Domain.Persistence;
using Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IApplicationDbContextFactory _dbContextFactory;
        private readonly int _notificationLimit = 100;

        public NotificationService(IApplicationDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public void Push(Notification notification)
        {
            using var context = _dbContextFactory.CreateDbContext();

            context.Notifications.Add(notification);
            context.SaveChanges();
        }

        public List<Notification> Read(int userId)
        {
            using var context = _dbContextFactory.CreateDbContext();

            var unreadCount = context.Notifications.Count(n => n.UserId == userId && n.Read == false);
            var limit = Math.Max(unreadCount, _notificationLimit);

            return context.Notifications.Where(n => n.UserId == userId)
                                        .OrderByDescending(n => n.TimeStamp)
                                        .Take(limit)
                                        .ToList();
        }
    }
}
