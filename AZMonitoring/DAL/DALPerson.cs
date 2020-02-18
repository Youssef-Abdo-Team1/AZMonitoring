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
        internal async Task<bool> AddPerson(Person newPerson)
        {
            try
            {
                await client.SetAsync(pathperson + newPerson.ID, newPerson);
                return true;
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return false; }
        }
        internal async Task<bool> UpdatePerson(Person person)
        {
            try
            {
                await client.UpdateAsync(pathperson + person.ID, person);
                return true;
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return false; }
        }
        internal async Task<bool> UpdatePersonPosition(string id,string personposition)
        {
            try
            {
                await client.SetAsync(pathperson + id + "/IDPosition", personposition);
                return true;
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return false; }
        }
        internal async Task<string> GetPositionID(string PersonID)
        {
            try
            {
                var snap = await client.GetAsync(pathperson + PersonID + "/Position");
                return snap.ResultAs<string>();
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return null; }
        }
        internal async Task<Person> GetLogedPerson(string PersonID,string password)
        {
            try
            {
                var snap = await client.GetAsync(pathperson +  PersonID + "/Password");
                if(password == snap.ResultAs<string>())
                {
                    return (await client.GetAsync(pathperson + PersonID)).ResultAs<Person>();
                }
                return null;
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return null; }
        }
        internal async Task<Person> GetPersonbyID(string PersonID)
        {
            try
            {
                var snap = await client.GetAsync(pathperson + PersonID);
                var p = snap.ResultAs<Person>();
                p.Password = p.SSN = "";
                p.ChatsID = null;
                return p;
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return null; }
        }
        internal async Task<bool> AvailableID(string PersonID)
        {
            try
            {
                var snap = await client.GetAsync(pathperson + PersonID);
                var p = snap.ResultAs<Person>();
                if (p == null) { return true; }
                return false;
            }
            catch (Exception ex) { return false; }
        }
        internal async Task<bool> AddChatToPerson(string PersonID, string chatid)
        {
            try
            {
                var snap = await client.GetAsync(pathperson + PersonID + "/ChatsID");
                var chts = snap.ResultAs<List<string>>();
                chts.Add(chatid);
                await client.UpdateAsync(pathperson + PersonID + "/ChatsID", chts);
                return true;
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return false; }
        }
        internal async Task<string> AddPersonImage(string PersonID, FileStream img)
        {
            try
            {
                var auth = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyA4FzwRW0vN6Q6IEVJf_GFOAre0uj4zr44"));
                var a = await auth.SignInAnonymouslyAsync();
                return await statics.UploadImage(PersonID, img, "fir-test1-fb35d.appspot.com", a);
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return ""; }
        }
        internal async Task<Person> GetPersonbyPositionID(string PositionID)
        {
            try
            {
                return await GetPersonbyID(await GetPositionPersonIDByID(PositionID));
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return null; }
        }
        internal async Task<bool> DeletePersonbyID(string ID)
        {
            try
            {
                await client.DeleteAsync(pathperson + ID);
                return true;
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return false; }
        }
    }
}
