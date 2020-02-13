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

        private static IFirebaseClient client;
        private string pathperson = "AZMonitoring/Person/",
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
            pathchat = "AZMonitoring/Chat/",
            pathmessage = "AZMonitoring/Message/";
        private readonly static IFirebaseConfig Config = new FireSharp.Config.FirebaseConfig { AuthSecret = "IZLDtMrlpCiu25KHovLrbZzLirslRdoTvuj7wsZ7", BasePath = "https://fir-test1-fb35d.firebaseio.com/" };
        public void CreateConnection(string emailAddress = null,string password = null)
        {
            try
            {
                client = new FireSharp.FirebaseClient(Config);
            }
            catch(Exception ex) { MessageBox.Show($"الخطأ: \n{ex.Message}", "حدث خطأ اثناء الاتصال", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        public async void test()
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
        public async void test_addProvinces()
        {
            await AddProvince(new Province { Name = "القاهرة", AdministrationsID = new List<string>() { "", "" }, CulturalAgentDGID = "", Description = "", HCAdministrationID = "", InstructsID = new List<string>() { "", "" }, LegalAgentDGID = "", SWelfareDID = "" });
            await AddProvince(new Province { Name = "الجيزة", AdministrationsID = new List<string>() { "", "" }, CulturalAgentDGID = "", Description = "", HCAdministrationID = "", InstructsID = new List<string>() { "", "" }, LegalAgentDGID = "", SWelfareDID = "" });
            await AddProvince(new Province { Name = "الاسكندرية", AdministrationsID = new List<string>() { "", "" }, CulturalAgentDGID = "", Description = "", HCAdministrationID = "", InstructsID = new List<string>() { "", "" }, LegalAgentDGID = "", SWelfareDID = "" });
            await AddProvince(new Province { Name = "الفيوم", AdministrationsID = new List<string>() { "", "" }, CulturalAgentDGID = "", Description = "", HCAdministrationID = "", InstructsID = new List<string>() { "", "" }, LegalAgentDGID = "", SWelfareDID = "" });
            await AddProvince(new Province { Name = "البحيرة", AdministrationsID = new List<string>() { "", "" }, CulturalAgentDGID = "", Description = "", HCAdministrationID = "", InstructsID = new List<string>() { "", "" }, LegalAgentDGID = "", SWelfareDID = "" });
            await AddProvince(new Province { Name = "المنصورة", AdministrationsID = new List<string>() { "", "" }, CulturalAgentDGID = "", Description = "", HCAdministrationID = "", InstructsID = new List<string>() { "", "" }, LegalAgentDGID = "", SWelfareDID = "" });
        }
    }

    class Product
    {
        public string Name { get; set; }
        public List<string> sizes { get; set; }
        public List<int> prices { get; set; }
    }
}
