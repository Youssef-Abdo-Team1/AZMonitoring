using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AZMonitoring
{
    public class GInstruct
    {
        public string GeneralInstructorID { get; set; }
        public string FirstInstructorID { get; set; }
        public List<string> HeadsID { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IDProvince { get; set; }

        public List<string> InstructsID
        {
            get => default;
            set
            {
            }
        }
    }
}