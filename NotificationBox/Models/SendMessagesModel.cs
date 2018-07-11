using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationBox.Models
{
    public class SendMessagesModel
    {
        public int MessagesSent { get; set; }

        public List<MessageErrorModel> Errors { get; }

        public SendMessagesModel()
        {
            Errors = new List<MessageErrorModel>();
        }
    }
}
