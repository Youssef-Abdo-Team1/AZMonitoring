using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZMonitoring.DAL
{
    partial class DAL
    {
        internal async void AddChat(Chat newchat)
        {
            try
            {
                newchat.ID = (await client.PushAsync(pathchat, newchat)).Result.name;
                UpdateID(pathchat + newchat.ID, newchat.ID);
                AddChatstoPerson(new Chats { IDChat = newchat.ID, IDPerson = newchat.IDPerson2 }, newchat.IDPerson1).Start();
                AddChatstoPerson(new Chats { IDChat = newchat.ID, IDPerson = newchat.IDPerson1 }, newchat.IDPerson2).Start();
            }
            catch(Exception ex) { }
        }

    }
}
