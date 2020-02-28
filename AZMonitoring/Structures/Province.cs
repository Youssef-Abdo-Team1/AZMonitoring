using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AZMonitoring
{
    public class Province
    {
        public StaticInfo HCAdministrationID { get; set; }
        public StaticInfo LegalAgentDGID { get; set; }
        public StaticInfo CulturalAgentDGID { get; set; }
        public StaticInfo SWelfareDID { get; set; }
        public string Name { get; set; }
        public List<string> GInstructsID { get; set; }
        public string Description { get; set; }

        public List<string> AdministrationsID { get; set; }
    }
}