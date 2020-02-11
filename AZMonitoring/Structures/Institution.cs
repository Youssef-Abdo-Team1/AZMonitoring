using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AZMonitoring
{
    public class Institution
    {
        public Stages Stage { get; set; }
        public Type Type { get; set; }
        public string SheikhID { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> TeachersID { get; set; }
        public int StudentsCount { get; set; }
        public int ClassesCount { get; set; }
        public string IDAdministration { get; set; }
    }
}