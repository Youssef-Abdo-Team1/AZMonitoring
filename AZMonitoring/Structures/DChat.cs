using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static AZMonitoring.statics;

namespace AZMonitoring
{
    public class DChat : Chat
    {
        public string Name { get; set; }
        public string Photo { get; set; }
        public string Position { get; set; }
        public async Task Initialize()
        {
            if(IDPerson1 != statics.LogedPerson.ID)
            {
                Photo = await DB.GetPersonPhoto(IDPerson1);
                Name = await DB.GetPersonName(IDPerson1);
                Position = await DB.GetPositionNameByID(await DB.GetPositionID(IDPerson1));
            }
            else
            {
                Photo = await DB.GetPersonPhoto(IDPerson2);
                Name = await DB.GetPersonName(IDPerson2);
                Position = await DB.GetPositionNameByID(await DB.GetPositionID(IDPerson2));
            }
        }
        public async static Task<DChat> GetDChat(Chat chat)
        {
            var d = new DChat();
            d.ID = chat.ID;
            d.IDPerson1 = chat.IDPerson1;
            d.IDPerson2 = chat.IDPerson2;
            await d.Initialize();
            return d;
        }
    }
}