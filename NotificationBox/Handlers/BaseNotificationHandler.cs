using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotificationBox.Messages;

namespace NotificationBox.Handlers
{
    public abstract class BaseNotificationHandler : INotificationHandler
    {
        public string Name { get; set; }

        public virtual Type GetMessageType()
        {
            return GetType().GetInterfaces().Where(i => i.IsGenericType).SelectMany(i => i.GenericTypeArguments).FirstOrDefault();
        }

        public abstract Task<Tuple<bool, string>> SendMessage(IMessage message);

        public virtual void Dispose()
        {
        }
    }
}
