using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AZMonitoring
{
    public class Job
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public List<string> WorksID { get; set; }
        public int Description { get; set; }
    }
}