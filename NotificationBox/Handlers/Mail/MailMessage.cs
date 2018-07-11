using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationBox.Handlers
{
    public class MailMessage : IMessage
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public bool IsHTML { get; set; }

        public string From { get; set; }

        public string Subject { get; set; }

        public string To { get; set; }

        public string Message { get; set; }
    }
}
