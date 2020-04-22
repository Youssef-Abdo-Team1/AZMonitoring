using Firebase.Database;
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
        IDisposable y,x;
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
        internal void AddMessage(DChat Chat, Message message)
        {
            try
            {
                string pathmessage = pathchat + Chat.ID + "/Messages/" + Chat.MessagesCounter;
                //if(Chat.DMessages == null || Chat.DMessages.Count < 1) { Chat.DMessages = new List<DMessage>(); }
                client.SetAsync(pathmessage, message);
                Chat.MessagesCounter++;
                client.UpdateAsync(pathchat + Chat.ID + "/MessagesCounter", Chat.MessagesCounter);
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
                var x = (await client.GetAsync(pathchat + ID)).ResultAs<Chat>();
                return x;
            }
            catch (Exception ex) { MessageBox.Show($"الخطأ: \n{ex.Message}", "حدث خطأ اثناء الاتصال", MessageBoxButton.OK, MessageBoxImage.Error); return null; }
        }
        internal async Task<List<DChat>> GetChats(List<string> IDs)
        {
            var ls = new List<DChat>();
            foreach (var item in IDs)
            {
                 ls.Add(DChat.GetDChat(await GetChat(item)));
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
        internal void SetChatsListener(string userid)
        {
            try
            {
                x = firebase.Child($"{pathperson}{userid}/Chats")
                    .AsObservable<string>()
                    .Subscribe(async d =>
                    statics.DChats.Add(DChat.GetDChat(await GetChat("dsf")))
                    );
            }
            catch { }
        }
        internal void ClearLisner()
        {
            x.Dispose();
            x = null;
        }
        internal void SetMessagesListener(string id)
        {
            try
            {
                
                y = firebase.Child($"AZMonitoring/Chat/{id}/Messages")
                    .AsObservable<Message>()
                    .Subscribe(d => 
                    statics.DChats.FirstOrDefault(item => item.ID == id)
                    .DMessages.Add(DMessage.GetDMessage(d.Object)));
                //client.ListenAsync
                //y = await client.OnAsync(pathchat + id + "/MessagesID"
                ////    , changed: async (obj, snap, cont) => {
                ////    if (statics.CurrentChat.Messages == null) { statics.CurrentChat.Messages = new List<DMessage>(); }
                ////    if (statics.CurrentChat.MessagesID == null) { statics.CurrentChat.MessagesID = new List<string>(); }
                ////    statics.CurrentChat.MessagesID.Add(snap.Data);
                ////    statics.CurrentChat.Messages.Add(DMessage.GetDMessage(await GetMessage(snap.Data)));
                ////    statics.MessageRefreshDelegate.Invoke();
                ////}
                //, added: (FireSharp.EventStreaming.ValueAddedEventHandler)(async (obj, snap, cont) =>
                //{
                //    if (statics.CurrentChat.DMessages == null) { statics.CurrentChat.DMessages = new List<DMessage>(); }
                //    if (statics.CurrentChat.DMessages == null) { statics.CurrentChat.DMessages = new List<string>(); }
                //    statics.CurrentChat.DMessages.Add((string)snap.Data);
                //    statics.CurrentChat.DMessages.Add(DMessage.GetDMessage(await GetMessage(snap.Data)));
                //    statics.MessageRefreshDelegate.Invoke();
                //})
                //);
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
