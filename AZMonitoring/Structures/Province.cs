using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AZMonitoring
{
    public class Province
    {
        public StaticProvinceInfo HCAdministrationID { get; set; }
        public StaticProvinceInfo LegalAgentDGID { get; set; }
        public StaticProvinceInfo CulturalAgentDGID { get; set; }
        public StaticProvinceInfo SWelfareDID { get; set; }
        public string Name { get; set; }
        public List<string> GInstructsID { get; set; }
        public string Description { get; set; }

        public List<string> AdministrationsID { get; set; }
    }
}