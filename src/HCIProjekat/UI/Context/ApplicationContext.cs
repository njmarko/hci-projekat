using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using UI.Context.Routers;
using UI.Context.Stores;

namespace UI.Context
{
    public class ApplicationContext : IApplicationContext
    {
        public IRouter Router { get; set; }
        public IStore Store { get; set; }
        public Notifier Notifier { get; set; }

        public ApplicationContext(IRouter router, IStore store)
        {
            Router = router;
            Store = store;
            Notifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: Application.Current.MainWindow,
                    corner: Corner.BottomRight,
                    offsetX: 20,
                    offsetY: 20);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(3),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(5));

                cfg.Dispatcher = Application.Current.Dispatcher;
            });
        }
    }
}
