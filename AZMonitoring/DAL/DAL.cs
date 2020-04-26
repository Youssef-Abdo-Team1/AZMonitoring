using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Firebase.Database;
using FireSharp.Config;
using FireSharp.EventStreaming;
using FireSharp.Exceptions;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;

namespace AZMonitoring.DAL
{
    partial class DAL
    {
        private readonly string pathperson = "AZMonitoring/Person/",
            pathprovince = "AZMonitoring/Province/Provinces/",
            pathprovincenames = "AZMonitoring/Province/Names/",
            pathadministration = "AZMonitoring/Administration/",
            pathadmin = "AZMonitoring/Admin/",
            pathginstruct = "AZMonitoring/GInstruct/",
            pathinstruct = "AZMonitoring/Instruct/",
            pathinstitution = "AZMonitoring/Institution/",
            pathposition = "AZMonitoring/Position/",
            pathjob = "AZMonitoring/Job/",
            pathwork = "AZMonitoring/Work/",
            pathchat = "AZMonitoring/Chat/";
        private readonly FirebaseClient firebase = new FirebaseClient("https://fir-test1-fb35d.firebaseio.com/",
          new FirebaseOptions
          {
              AuthTokenAsyncFactory = () => Task.FromResult("IZLDtMrlpCiu25KHovLrbZzLirslRdoTvuj7wsZ7")
          });
        public DAL()
        {

        }
        internal void SetOnline(string userid)
        {
            try
            {
                client.SetAsync(pathperson + userid + "/Online", true);
            }
            catch (Exception ex) { MessageBox.Show($"الخطأ: \n{ex.Message}", "حدث خطأ اثناء الاتصال", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        internal async Task SetOffline(string userid)
        {
            try
            {
                await client.SetAsync(pathperson + userid + "/Online", DateTime.Now);
            }
            catch (Exception ex) { MessageBox.Show($"الخطأ: \n{ex.Message}", "حدث خطأ اثناء الاتصال", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        private static MainWindow Main { get; set; }

        private static IFirebaseClient client;


        internal void CreateConnection(MainWindow main,string emailAddress = null,string password = null)
        {
            try
            {
                client = new FireSharp.FirebaseClient(new FirebaseConfig { AuthSecret = "IZLDtMrlpCiu25KHovLrbZzLirslRdoTvuj7wsZ7", BasePath = "https://fir-test1-fb35d.firebaseio.com/" });
                Main = main;
                SetProvinceListener();
            }
            catch(Exception ex) { MessageBox.Show($"الخطأ: \n{ex.Message}", "حدث خطأ اثناء الاتصال", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        internal void UpdateID(string path,string id)
        {
            try
            {
                Task.Run(() => { client.Set(path + "/ID", id); });
            }
            catch { }
        }

        internal async void test_addinstituation()
        {
            await AddInstitution(new Institution { Name = "معهد حفيظة الألفي", StudentsCount = 455, IDAdministration = "-M1_7qiFwCUGM3lU5DE4", TeachersID = new List<string> { "M1_CEt1YBUr5YHpOh8e" }, Stage = Stages.PrimaryStage, Type = Type.Normal, SheikhID = "-M1_CDQFwQS5CAs7SBq_" });
        }
        internal async void Test_addpersons()
        {
            await AddPerson(new Person { ID = "1515", Name = " وفاء ابراهيم", Password = "123", Photo = "https://firebasestorage.googleapis.com/v0/b/fir-test1-fb35d.appspot.com/o/IMG-20200304-WA0003.jpg?alt=media&token=56834b99-d3b5-42a7-85c3-400f270ddfa8&fbclid=IwAR3XtcS36TxmDDbpo-USok1ZzYQ03_JtyJbEjykeYTXQKkTu65e8N_3CTiQ" });
            await AddPerson(new Person { ID = "7666", Name = "سعيد سالم", Password = "123" });
            await AddPerson(new Person { ID = "7555", Name = "صفاء شوقي", Password = "123" });
            await AddPerson(new Person { ID = "6665", Name = "احمد رجب", Password = "123" });
            await AddPerson(new Person { ID = "2545", Name = "عبد الرحمن ابوالدهب", Password = "123" });
            await AddPerson(new Person { ID = "1475", Name = "سعيد سالم", Password = "123" });
            await AddPerson(new Person { ID = "2647", Name = "عبدالرحمن حسين فهمي", Password = "123" });
            await AddPerson(new Person { ID = "96587", Name = "محمد رأفت", Password = "123" });
            await AddPerson(new Person { ID = "7666", Name = "سحر عبدالله", Password = "123" });
        }
        internal async void test_add_positions()
        {
            await AddPosition(new Position { Name = "شيخ المعهد", Level = 6, PersonID = "7666", IDProvince = "القاهرة" });
            await AddPosition(new Position { Name = "معلم التربية الرياضية", Level = 6, PersonID = "96587", IDProvince = "القاهرة" });
        }

        internal async void test_addChats()
        {
            await AddChat(new Chat { IDPerson1 = "66", IDPerson2 = "77" });
            await AddChat(new Chat { IDPerson1 = "88", IDPerson2 = "77" });
            await AddChat(new Chat { IDPerson1 = "66", IDPerson2 = "88" });
            await AddChat(new Chat { IDPerson1 = "1222", IDPerson2 = "88" });
            await AddChat(new Chat { IDPerson1 = "66", IDPerson2 = "1222" });
            await AddChat(new Chat { IDPerson1 = "1222", IDPerson2 = "77" });
        }
        internal async void test_addGinstruct()
        {
            Person gp = await GetPersonbyID("1475");
            Person p = await GetPersonbyID("7555");
            Person a1 = await GetPersonbyID("78965");
            Person a2 = await GetPersonbyID("6665");
            Person a3 = await GetPersonbyID("2545");
            var gi = new GInstruct
            {
                Name = "توجيه العلوم",
                Description = "الاهتمام بالشؤن الرياضيه",
                IDProvince = "القاهرة",
            };
            await AddGInstruct(gi);
        }
        internal async void test_addinstruct()
        {
            await AddInstruct(new Instruct { IDAdministration = "-M0dkQvTDnlH9WOHfCEq", IDGInstruct = "-M0jpVfbxZO5PVBQ6v6G", AdminstrationInstructorsID = new List<string> {"66", "77" } });
        }
        internal async void test_addadministrations()
        {
            await AddAdministration(new Administration { IDProvince = "القاهرة", Name = "جنوب" });
            await AddAdministration(new Administration { IDProvince = "القاهرة", Name = "شرق" });
            await AddAdministration(new Administration { IDProvince = "القاهرة", Name = "غرب" });
            await AddAdministration(new Administration { IDProvince = "القاهرة", Name = "شمال" });
        }
    }
}
