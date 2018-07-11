using NotificationBox.Handlers;
using NotificationBox.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace NotificationBox.Services
{
    public interface INotificationHandlerService
    {
        void LoadNotificationHandlers();

        INotificationHandler GetHandler(string name);
    }

    public class NotificationHandlerService : INotificationHandlerService
    {
        private Dictionary<string, Type> handlers;

        public NotificationHandlerService()
        {
            handlers = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);
        }

        public INotificationHandler GetHandler(string name)
        {
            if (handlers.ContainsKey(name))
            {
                var keyValuePair = handlers.FirstOrDefault(kvp => kvp.Key.Equals(name, StringComparison.OrdinalIgnoreCase));

                INotificationHandler handler = Activator.CreateInstance(keyValuePair.Value) as INotificationHandler;
                if (handler != null)
                {
                    handler.Name = keyValuePair.Key;
                }
                return handler;
            }

            return null;
        }

        public void LoadNotificationHandlers()
        {
            var types = Assembly.GetExecutingAssembly().DefinedTypes.Where(t => t.ImplementedInterfaces.Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(INotificationHandler<>)));

            string interfaceName = typeof(INotificationHandler<>).Name;
            interfaceName = interfaceName.Substring(1, interfaceName.LastIndexOf('`') - 1);

            foreach (var type in types)
            {
                if (type.Name.EndsWith(interfaceName, StringComparison.OrdinalIgnoreCase))
                {
                    handlers.Add(type.Name.Substring(0, type.Name.Length - interfaceName.Length), type);
                }
                else
                {
                    handlers.Add(type.Name, type);
                }
            }
        }
    }
}
