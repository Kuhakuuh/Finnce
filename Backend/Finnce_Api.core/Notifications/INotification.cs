namespace Finnce_Api.core.Notifications
{
    public interface INotification
    {
        public Task SendMessage(Notification notification);
    }
}
