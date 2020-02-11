using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AZMonitoring
{
    public class Message
    {
        public string ID { get; set; }
        public MessageType Type { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }
}