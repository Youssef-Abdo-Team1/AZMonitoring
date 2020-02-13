using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AZMonitoring
{
    public class statics
    {
        public static Frame staticframe { get; set; }
        public static async Task<ImageSource> DounloadImage(string Photo)
        {
            try
            {
                if (Photo == "") { return null; }
                using (WebClient webClient = new WebClient())
                {
                    byte[] data = await webClient.DownloadDataTaskAsync(Photo);

                    using (MemoryStream mem = new MemoryStream(data))
                    {
                        return BitmapFrame.Create(mem, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                    }
                }
            }
            catch { return null; }
        }
    }
}
