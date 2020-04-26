using Firebase.Database;
using FireSharp.Response;
using Newtonsoft.Json;
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
        IDisposable y;
        EventStreamResponse x;
        internal async Task<string> AddChat(Chat newchat)
        {
            try
            {
                newchat.ID = (await client.PushAsync(pathchat, newchat)).Result.name;
                UpdateID(pathchat + newchat.ID, newchat.ID);
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
                var x = statics.LogedPerson.Chats.Where(item => item == p);
                if (x.Count() == 0)
                {
                    return await AddChat(new Chat());
                }
                else { return x.First(); }
            }
            catch (Exception ex) { MessageBox.Show($"الخطأ: \n{ex.Message}", "حدث خطأ اثناء الاتصال", MessageBoxButton.OK, MessageBoxImage.Error); return ""; }
        }
        internal async void AddMessage(string Chatid, Message message)
        {
            try
            {
                await firebase.Child(pathchat + Chatid + "/Messages/").PostAsync(JsonConvert.SerializeObject(message));
                //await client.PushAsync(pathchat + Chatid + "/Messages/", message);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"الخطأ: \n{ex.Message}", "حدث خطأ اثناء الاتصال", MessageBoxButton.OK, MessageBoxImage.Error);
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
        internal async Task<List<DChat>> GetChats(List<string> IDs)
        {
            var ls = new List<DChat>();
            foreach (var item in IDs)
            {
                 ls.Add(await DChat.GetDChat(await GetChat(item)));
            }
            return ls;
        }
        //internal async Task<Message> GetMessage(string id)
        //{
        //    try
        //    {
        //        return (await client.GetAsync(pathmessage + id)).ResultAs<Message>();
        //    }
        //    catch (Exception ex) { MessageBox.Show($"الخطأ: \n{ex.Message}", "حدث خطأ اثناء الاتصال", MessageBoxButton.OK, MessageBoxImage.Error); return null; }
        //}
        //internal async Task<List<Message>> GetMessages(List<string> messagesID)
        //{
        //    try
        //    {
        //        if(messagesID != null && messagesID.Count > 0)
        //        {
        //            var ls = new List<Message>();
        //            foreach (var item in messagesID)
        //            {
        //                ls.Add(await GetMessage(item));
        //            }
        //            return ls;
        //        }
        //        return null;
        //    }
        //    catch (Exception ex) { MessageBox.Show($"الخطأ: \n{ex.Message}", "حدث خطأ اثناء الاتصال", MessageBoxButton.OK, MessageBoxImage.Error); return null; }
        //}
        internal async void SetChatsListener(string userid,AddChatDelegate @delegate)
        {
            try
            {
                x = await client.OnAsync($"{pathperson}{userid}/Chats", changed: async (ss, snap, ds) => { @delegate.Invoke(await GetChat(snap.Data)); });
            }
            catch { }
        }
        internal void ClearLisner()
        {
            x?.Dispose();
        }
        internal void SetMessagesListener(string id, AddMessageDelegate @delegate)
        {
            try
            {

                y = firebase.Child($"AZMonitoring/Chat/{id}/Messages")
                    .AsObservable<Message>()
                    .Subscribe(d => @delegate.Invoke(d.Object));
            }
            catch { }
        }

        internal void ClearMessageLisner()
        {
            y?.Dispose();
        }
    }
}
