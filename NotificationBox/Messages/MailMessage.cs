using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationBox.Messages
{
    public class MailMessage : IMessage
    {
        public string From { get; set; }

        public string Subject { get; set; }

        public string To { get; set; }

        public string Message { get; set; }
    }
}
