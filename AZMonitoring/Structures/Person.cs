using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AZMonitoring
{
    public class Person
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Photo { get; set; }
        public List<Chats> Chats { get; set; }
        public string IDPosition { get; set; }
        public string Description { get; set; }
        public string SSN { get; set; }
        public DateTime DOB { get; set; }
    }
}