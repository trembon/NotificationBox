using NotificationBox.Messages;
using NotificationBox.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
