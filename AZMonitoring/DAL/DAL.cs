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
        private readonly static IFirebaseConfig Config = new FireSharp.Config.FirebaseConfig { AuthSecret = "IZLDtMrlpCiu25KHovLrbZzLirslRdoTvuj7wsZ7", BasePath = "https://fir-test1-fb35d.firebaseio.com/" };
        public void CreateConnection(string emailAddress = null,string password = null)
        {
            try
            {
                client = new FireSharp.FirebaseClient(Config);
            }
            catch(Exception ex) { MessageBox.Show($"الخطأ: \n{ex.Message}", "حدث خطأ اثناء الاتصال", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
}
