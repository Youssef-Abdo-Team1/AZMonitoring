using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AZMonitoring
{
    public class Province
    {
        public String HCAdministrationID { get; set; }
        public string LegalAgentDGID { get; set; }
        public string CulturalAgentDGID { get; set; }
        public string SWelfareDID { get; set; }
        public string Name { get; set; }
        public List<string> InstructsID { get; set; }
        public string Description { get; set; }

        public List<string> AdministrationsID
        {
            get => default;
            set
            {
            }
        }
    }
}