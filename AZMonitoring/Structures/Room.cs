using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AZMonitoring
{
    public class Room
    {
        public string ID { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatorID { get; set; }
        public string GChatID { get; set; }
        public List<string> PersonSID { get; set; }
    }
}