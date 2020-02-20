using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
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


        private static MainWindow Main { get; set; }

        private static IFirebaseClient client;
        private readonly static IFirebaseConfig Config = new FireSharp.Config.FirebaseConfig { AuthSecret = "IZLDtMrlpCiu25KHovLrbZzLirslRdoTvuj7wsZ7", BasePath = "https://fir-test1-fb35d.firebaseio.com/" };


        internal void CreateConnection(MainWindow main,string emailAddress = null,string password = null)
        {
            try
            {
                client = new FireSharp.FirebaseClient(Config);
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



        internal async void Test()
        {
            var x = new Product
            {
                Name = "abc",
                sizes = new List<string>(),
                prices = new List<int>()
            };
            x.sizes.Add("s");
            x.sizes.Add("M");
            x.sizes.Add("L");
            x.sizes.Add("xL");
            x.prices.Add(56);
            x.prices.Add(53);
            x.prices.Add(52);
            x.prices.Add(53);
            await client.SetAsync("test/clothing/abc", x);
            var y = (await client.GetAsync("test/clothing/abc")).ResultAs<Product>();
            y.sizes.Add("xxL");
            y.prices.Add(65);
            await client.UpdateAsync("test/clothing/abc", y);
        }
        internal async void Test_addProvinces()
        {
            
        }
        internal async void Test_addpersons()
        {
            await AddPerson(new Person { ID = "1222", Name = "يوسف شعبان", Password = "123" });
            await AddPerson(new Person { ID = "66", Name = "احمد محمد", Password = "123" });
            await AddPerson(new Person { ID = "77", Name = "خالد عبدالله", Password = "123" });
            await AddPerson(new Person { ID = "88", Name = "محمد محمود", Password = "123" });
        }
        internal async void test_add_positions()
        {
            string s1 = await AddPosition(new Position { Name = "jkhlk", Level = 6 });
            string s2= await AddPosition(new Position { Name = "jkdsfhlk", Level = 6 });
            string s3 = await AddPosition(new Position { Name = "jsaddsfkhlk", Level = 6 });
            string s4 = await AddPosition(new Position { Name = "jkhfdgfdglk", Level = 6 });
        }

        internal void test_addChats()
        {
            AddChat(new Chat { IDPerson1 = "66", IDPerson2 = "77" });
            AddChat(new Chat { IDPerson1 = "88", IDPerson2 = "77" });
            AddChat(new Chat { IDPerson1 = "66", IDPerson2 = "88" });
            AddChat(new Chat { IDPerson1 = "1222", IDPerson2 = "88" });
            AddChat(new Chat { IDPerson1 = "66", IDPerson2 = "1222" });
            AddChat(new Chat { IDPerson1 = "1222", IDPerson2 = "77" });
        }
    }

    class Product
    {
        public string Name { get; set; }
        public List<string> sizes { get; set; }
        public List<int> prices { get; set; }
    }
}
