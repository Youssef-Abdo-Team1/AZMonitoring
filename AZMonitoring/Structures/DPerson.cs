using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AZMonitoring
{
    public class DPerson : Person 
    {
        public string SubName { get { return Name.Split(' ').First(); } }
        internal static DPerson GetDPerson(Person p)
        {
            return new DPerson
                {
                    Chats = p.Chats,
                    Description = p.Description,
                    DOB = p.DOB,
                    ID = p.ID,
                    IDPosition = p.IDPosition,
                    Name = p.Name,
                    Password = p.Password,
                    Photo = p.Photo,
                    SSN = p.SSN
                };
        }
    }
}