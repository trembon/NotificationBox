using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationBox.Messages
{
    public class SlackMessage : IMessage
    {
        public string Channel { get; set; }

        public string Username { get; set; }

        public string Emoji { get; set; }

        public string Message { get; set; }

        public string WebHookURL { get; set; }
    }
}
