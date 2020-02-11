using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AZMonitoring
{
    public class Administration
    {
        public List<string> InstitutionsID { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<string> InstrutsID
        {
            get => default;
            set
            {
            }
        }

        public string IDProvince
        {
            get => default;
            set
            {
            }
        }
    }
}