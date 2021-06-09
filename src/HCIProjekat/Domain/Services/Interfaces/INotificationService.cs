using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Interfaces
{
    public interface INotificationService
    {
        void Push(Notification notification);
        void Seen(int userId);
        Notification ReadNotification(int notificationId);
        List<Notification> Read(int userId);
    }
}
