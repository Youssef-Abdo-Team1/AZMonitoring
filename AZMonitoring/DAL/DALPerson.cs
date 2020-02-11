using Firebase.Auth;
using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AZMonitoring.DAL
{
    partial class DAL
    {
        public async Task<bool> AddPerson(Person newPerson)
        {
            try
            {
                await client.SetAsync($"AZMonitoring/Person/{newPerson.ID}", newPerson);
                await client.SetAsync($"AZMonitoring/Person/{newPerson.ID}/Chats", "");
                return true;
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return false; }
        }
        public async Task<bool> UpdatePersonName(Person person)
        {
            try
            {
                await client.UpdateAsync($"AZMonitoring/Person/{person.ID}", person);
                return true;
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return false; }
        }
        public async Task<string> GetPositionID(string PersonID)
        {
            try
            {
                var snap = await client.GetAsync($"AZMonitoring/Person/{PersonID}/Position");
                return snap.ResultAs<string>();
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return null; }
        }
        public async Task<Person> GetLogedPerson(string PersonID,string password)
        {
            try
            {
                var snap = await client.GetAsync($"AZMonitoring/Person/{PersonID}/Password");
                if(password == snap.ResultAs<string>())
                {
                    return (await client.GetAsync($"AZMonitoring/Person/{PersonID}")).ResultAs<Person>();
                }
                return null;
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return null; }
        }
        public async Task<Person> GetPersonbyID(string PersonID)
        {
            try
            {
                var snap = await client.GetAsync($"AZMonitoring/Person/{PersonID}");
                var p = snap.ResultAs<Person>();
                p.Password = p.SSN = "";
                p.ChatsID = null;
                return p;
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return null; }
        }
        public async Task<bool> AvailableID(string PersonID)
        {
            try
            {
                var snap = await client.GetAsync($"AZMonitoring/Person/{PersonID}");
                var p = snap.ResultAs<Person>();
                if (p == null) { return true; }
                return false;
            }
            catch (Exception ex) { return false; }
        }
        public async Task<bool> AddChatToPerson(string PersonID, string chatid)
        {
            try
            {
                var snap = await client.GetAsync($"AZMonitoring/Person/{PersonID}/ChatsID");
                var chts = snap.ResultAs<List<string>>();
                chts.Add(chatid);
                await client.UpdateAsync($"AZMonitoring/Person/{PersonID}/ChatsID", chts);
                return true;
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return false; }
        }
        public async Task<bool> EditPersonImage(string PersonID, FileStream img)
        {
            try
            {
                var auth = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyA4FzwRW0vN6Q6IEVJf_GFOAre0uj4zr44"));
                var a = await auth.SignInAnonymouslyAsync();
                var task = new FirebaseStorage("fir-test1-fb35d.appspot.com", new FirebaseStorageOptions()
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true
                }).Child("AZMonitorimg").Child("Person").Child($"{PersonID}.jpg").PutAsync(img);
                await task;
                img.Close();
                return true;
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return false; }
        }
        public async Task<string> AddPersonImage(string PersonID, FileStream img)
        {
            try
            {
                var auth = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyA4FzwRW0vN6Q6IEVJf_GFOAre0uj4zr44"));
                var a = await auth.SignInAnonymouslyAsync();
                var task = new FirebaseStorage("fir-test1-fb35d.appspot.com", new FirebaseStorageOptions()
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true
                }).Child("AZMonitorimg").Child("Person").Child($"{PersonID}.jpg").PutAsync(img);
                var s = await task;
                img.Close();
                return s;
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return null; }
        }

    }
}
