using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AZMonitoring.DAL
{
    partial class DAL
    {
        EventStreamResponse x,y;
        internal async Task<string> AddChat(Chat newchat)
        {
            try
            {
                newchat.ID = (await client.PushAsync(pathchat, newchat)).Result.name;
                UpdateID(pathchat + newchat.ID, newchat.ID);
                AddChatstoPerson(new Chats { IDChat = newchat.ID, IDPerson = newchat.IDPerson2 }, newchat.IDPerson1);
                AddChatstoPerson(new Chats { IDChat = newchat.ID, IDPerson = newchat.IDPerson1 }, newchat.IDPerson2);
                return newchat.ID;
            }
            catch (Exception ex) { MessageBox.Show($"الخطأ: \n{ex.Message}", "حدث خطأ اثناء الاتصال", MessageBoxButton.OK, MessageBoxImage.Error); return ""; }
        }
        internal async Task<List<string>> GetChatAllMessagesID(string id)
        {
            try
            {
                return (await client.GetAsync(pathchat + id + "/MessagesID")).ResultAs<List<string>>();
            }
            catch { return null; }
        }
        internal async Task<string> AddNewChat(string p)
        {
            try
            {
                var x = statics.LogedPerson.Chats.Where(item => item.IDPerson == p);
                if (x.Count() == 0)
                {
                    return await AddChat(new Chat { IDPerson1 = statics.LogedPerson.ID, IDPerson2 = p });
                }
                else { return x.First().IDChat; }
            }
            catch (Exception ex) { MessageBox.Show($"الخطأ: \n{ex.Message}", "حدث خطأ اثناء الاتصال", MessageBoxButton.OK, MessageBoxImage.Error); return ""; }
        }
        internal async Task<bool> AddMessage(string ChatID, Message message)
        {
            try
            {

                var m = await GetChatAllMessagesID(ChatID);
                if(m == null || m.Count < 1) { m = new List<string>(); }
                message.ID = (await client.PushAsync(pathmessage, message)).Result.name;
                UpdateID(pathmessage + message.ID, message.ID);
                await client.SetAsync(pathchat + ChatID + "/MessagesID/" + m.Count, message.ID);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"الخطأ: \n{ex.Message}", "حدث خطأ اثناء الاتصال", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
        internal async Task<Chat> GetChat(string ID)
        {
            try
            {
                return (await client.GetAsync(pathchat + ID)).ResultAs<Chat>();
            }
            catch (Exception ex) { MessageBox.Show($"الخطأ: \n{ex.Message}", "حدث خطأ اثناء الاتصال", MessageBoxButton.OK, MessageBoxImage.Error); return null; }
        }
        internal async Task<Message> GetMessage(string id)
        {
            try
            {
                return (await client.GetAsync(pathmessage + id)).ResultAs<Message>();
            }
            catch (Exception ex) { MessageBox.Show($"الخطأ: \n{ex.Message}", "حدث خطأ اثناء الاتصال", MessageBoxButton.OK, MessageBoxImage.Error); return null; }
        }
        internal async Task<List<Message>> GetMessages(List<string> messagesID)
        {
            try
            {
                if(messagesID != null && messagesID.Count > 0)
                {
                    var ls = new List<Message>();
                    foreach (var item in messagesID)
                    {
                        ls.Add(await GetMessage(item));
                    }
                    return ls;
                }
                return null;
            }
            catch (Exception ex) { MessageBox.Show($"الخطأ: \n{ex.Message}", "حدث خطأ اثناء الاتصال", MessageBoxButton.OK, MessageBoxImage.Error); return null; }
        }
        internal async void SetChatsListener(string userid)
        {
            try { x = (await client.OnAsync(pathperson + userid + "/Chats", (obj, snap, cont) => {
                if(snap.Path.Split('/').Last() == "IDChat") { Main.Initialize_Chat(snap.Data); }
            },
            (obj, snap, cont) => {
                if (snap.Path.Split('/').Last() == "IDChat") { Main.Initialize_Chat(snap.Data); }
            }
            )); }
            catch { }
        }
        internal void ClearLisner()
        {
            x.Dispose();
            x = null;
        }
        internal async void SetMessagesListener(string id)
        {
            try
            {
                y = await client.OnAsync(pathchat + id + "/MessagesID", async (obj, snap, cont) => {
                    if (statics.CurrentChat.Messages == null) { statics.CurrentChat.Messages = new List<DMessage>(); }
                    if (statics.CurrentChat.MessagesID == null) { statics.CurrentChat.MessagesID = new List<string>(); }
                    statics.CurrentChat.MessagesID.Add(snap.Data);
                    statics.CurrentChat.Messages.Add(DMessage.GetDMessage(await GetMessage(snap.Data)));
                    statics.MessageRefreshDelegate.Invoke();
                });
            }
            catch { }
        }
        internal void ClearMessageLisner()
        {
            try {
                y.Dispose();
                y = null;
            }
            catch { }
        }
    }
}
