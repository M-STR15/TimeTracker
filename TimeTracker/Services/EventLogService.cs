using NotificationWpf;
using NotificationWpf.Models;

namespace TimeTracker.Services
{
    public class EventLogService
    {
        private NotificationManagement _notification;
        private string _versionSW;
        public EventLogService()
        {
            _notification = new NotificationManagement(5, 450);
        }

        public void WriteWarning(Guid guidId, Exception exception, string notificationMessange)
        {
            _notification.Create(eNotificationType.Warning, notificationMessange);
        }

        public void WriteError(Guid guidId, Exception exception, string notificationMessange)
        {
            _notification.Create(eNotificationType.Error, notificationMessange);
        }

        public void WriteInfo(Guid guidId, Exception exception, string notificationMessange)
        {
            _notification.Create(eNotificationType.Info, notificationMessange);
        }

        public void WriteSuscaise(Guid guidId, Exception exception, string notificationMessange)
        {
            _notification.Create(eNotificationType.Success, notificationMessange);
        }
    }
}
