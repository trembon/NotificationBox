using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using NotificationBox.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace NotificationBox.Services
{
    public interface IMessageParseService
    {
        IMessage Parse(Type messageType, IFormCollection parameters, Dictionary<string, string> defaultValues);
    }

    public class MessageParseService : IMessageParseService
    {
        public IMessage Parse(Type messageType, IFormCollection parameters, Dictionary<string, string> defaultValues)
        {
            IMessage instance = Activator.CreateInstance(messageType) as IMessage;

            Dictionary<string, PropertyInfo> properties = messageType.GetProperties().ToDictionary(k => k.Name.ToLowerInvariant(), v => v);
            foreach (PropertyInfo property in messageType.GetProperties())
            {
                if (parameters.TryGetValue(property.Name.ToLowerInvariant(), out StringValues values))
                {
                    property.SetValue(instance, Convert.ChangeType(values.FirstOrDefault(), property.PropertyType));
                }
                else if (defaultValues.ContainsKey(property.Name))
                {
                    property.SetValue(instance, Convert.ChangeType(defaultValues[property.Name], property.PropertyType));
                }
            }

            return instance;
        }
    }
}
