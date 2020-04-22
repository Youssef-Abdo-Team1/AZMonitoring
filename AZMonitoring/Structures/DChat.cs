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

namespace AZMonitoring
{
    public class DChat : Chat
    {
        DAL.DAL DB = new DAL.DAL();
        public string Name { get; set; }
        public string Photo { get; set; }
        public new List<DMessage> DMessages { get; set; }
        public string LastMessage { get { if (DMessages != null && DMessages.Count > 0) { return DMessages.LastOrDefault().Text; }return "لا يوجد رسائل"; } }
        public async void Initialize()
        {
            DMessages = new List<DMessage>();
            if(IDPerson1 != statics.LogedPerson.ID)
            {
                Photo = await DB.GetPersonPhoto(IDPerson1);
                Name = await DB.GetPersonName(IDPerson1);
            }
            else
            {
                Photo = await DB.GetPersonPhoto(IDPerson2);
                Name = await DB.GetPersonName(IDPerson2);
            }
            if (Messages != null)
            {
                foreach (var item in Messages)
                {
                    DMessages.Add(DMessage.GetDMessage(item));
                }
            }
            else { Messages = new List<Message>(); }
        }
        public static DChat GetDChat(Chat chat)
        {
            var d = new DChat();
            d.ID = chat.ID;
            d.IDPerson1 = chat.IDPerson1;
            d.IDPerson2 = chat.IDPerson2;
            d.MessagesCounter = chat.MessagesCounter;
            d.Messages = chat.Messages;
            //d.Initialize();
            return d;
        }
    }
}