using NotificationWpf.Models;
using NotificationWpf.Services;

namespace TimeTracker.Stories
{
    public class EventLogStory
    {
        public List<string> EventLogs { get; set; }
        private NotificationManagement _notificationService;
        public EventLogStory()
        {
            _notificationService = new NotificationManagement();
        }

        public void WriteInfo()
        {
            _notificationService.Create(eTypeNotification.Suscaise);

        }

        public void WriteWarning()
        {
            _notificationService.Create(eTypeNotification.Warning);
        }
        public void WriteError()
        {
            _notificationService.Create(eTypeNotification.Error);
        }

        public void WriteSuscaise()
        {
            _notificationService.Create(eTypeNotification.Suscaise);
        }
    }
}
