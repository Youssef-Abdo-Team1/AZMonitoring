using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AZMonitoring
{
    public class Chat
    {
        public string ID { get; set; }
        public string IDPerson1 { get; set; }
        public string IDPerson2 { get; set; }
        public List<Message> Messages { get; set; }
    }
}