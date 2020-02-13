using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZMonitoring.DAL
{
    partial class DAL
    {
        public async Task<bool> AddChat(Chat newchat)
        {
            try
            {
                await client.SetAsync(pathchat + newchat.ID, newchat);
                return true;
            }catch(Exception ex) { return false; }
        }
    }
}
