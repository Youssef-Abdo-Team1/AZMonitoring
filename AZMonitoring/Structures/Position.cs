using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AZMonitoring
{
    public class Position
    {
        public string PersonID { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public List<string> JobsID { get; set; }
        public string IDProvince { get; set; }
        public int Level { get; set; }
        public string IDInstruct { get; set; }
        public string IDInstitution { get; set; }
    }
}