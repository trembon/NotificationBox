using System;
using System.Threading.Tasks;

namespace NotificationBox.Handlers
{
    public interface INotificationHandler : IDisposable
    {
        string Name { get; set; }

        Task<Tuple<bool, string>> SendMessage(IMessage message);

        Type GetMessageType();
    }

    public interface INotificationHandler<TMessage> : INotificationHandler where TMessage : IMessage
    {
        Task<Tuple<bool, string>> SendMessage(TMessage message);
    }
}
