using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationBox.Models
{
    public class MessageModel
    {
        public Guid ID { get; set; }

        public IEnumerable<string> Targets { get; set; }

        public string Source { get; set; }

        public string To { get; set; }

        public string Message { get; set; }

        public IDictionary<string, string> Properties { get; set; }
    }
}
