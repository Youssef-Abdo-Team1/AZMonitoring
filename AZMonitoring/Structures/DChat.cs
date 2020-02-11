using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AZMonitoring
{
    public class DChat : Chat
    {
        public string Name { get; set; }
        public ImageSource Image { get; set; }
        public string Photo { get; set; }
        public List<DMessage> Messages { get; set; }
        public string LastMessage { get { return Messages.LastOrDefault().Text; } set { } }
        public void SetImage()
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    byte[] data = webClient.DownloadData(new Uri(Photo));

                    using (MemoryStream mem = new MemoryStream(data))
                    {
                        Image = BitmapFrame.Create(mem, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                    }
                }
            }
            catch { }
        }
        public async void InitializeMessages(string userID)
        {
            throw new System.NotImplementedException();
        }
    }
}