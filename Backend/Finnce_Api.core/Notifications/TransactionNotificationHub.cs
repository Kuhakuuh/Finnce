using Microsoft.AspNetCore.SignalR;

namespace Finnce_Api.core.Notifications
{
    public class TransactionNotificationHub : Hub<INotification>
    {
        public Task SuscribeToProduct(string transactionId)
        {
            return this.Groups.AddToGroupAsync(Context.ConnectionId, transactionId);
        }

    }
}
