﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationBox.Messages
{
    public interface IMessage
    {
        string Message { get; set; }
    }
}
