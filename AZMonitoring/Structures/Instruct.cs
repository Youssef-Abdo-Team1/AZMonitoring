using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AZMonitoring
{
    public class Instruct
    {
        public string ID { get; set; }
        public string IDAdministration { get; set; }
        public List<string> AdminstrationInstructorsID { get; set; }
        public string IDGInstruct { get; set; }
        public override string ToString()
        {
            return ID;
        }
    }
}