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
        internal async Task<string> AddChat(Chat newchat)
        {
            try
            {
                newchat.ID = (await client.PushAsync(pathchat, newchat)).Result.name;
                UpdateID(pathchat + newchat.ID, newchat.ID);
                AddChatstoPerson(new Chats { IDChat = newchat.ID, IDPerson = newchat.IDPerson2 }, newchat.IDPerson1).Start();
                AddChatstoPerson(new Chats { IDChat = newchat.ID, IDPerson = newchat.IDPerson1 }, newchat.IDPerson2).Start();
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
                m.Add((await client.PushAsync(pathmessage, message)).Result.name);
                await client.UpdateAsync(pathchat + ChatID + "/MessagesID/", m);
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
                var ls = new List<Message>();
                foreach (var item in messagesID)
                {
                    ls.Add(await GetMessage(item));
                }
                return ls;
            }
            catch (Exception ex) { MessageBox.Show($"الخطأ: \n{ex.Message}", "حدث خطأ اثناء الاتصال", MessageBoxButton.OK, MessageBoxImage.Error); return null; }
        }
    }
}
