using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AZMonitoring
{
    public class Message
    {
        public MessageType Type { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public string Sender { get; set; }
        public bool Read { get; set; }
    }
}