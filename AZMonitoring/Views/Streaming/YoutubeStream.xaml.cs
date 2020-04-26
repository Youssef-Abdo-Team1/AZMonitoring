using Microsoft.Toolkit.Win32.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AZMonitoring.Views.Streaming
{
    /// <summary>
    /// Interaction logic for YoutubeStream.xaml
    /// </summary>
    public partial class YoutubeStream : Page
    {
        string Link { get; set; }
        public YoutubeStream(string Link, string type)
        {
            InitializeComponent();
            if (type == "YoutubeID") { this.Link = "https://www.youtube-nocookie.com/embed/" + Link + "?rel=0&amp;showinfo=0"; }
            else if (type == "YoutubeLink")
            {
                if (Link.Contains("youtu.be/"))
                {
                    var s = Link.Split(new string[]{"/"}, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in s)
                    {
                        if(item.Length == 11)
                        {
                            this.Link = "https://www.youtube-nocookie.com/embed/" + item + "?rel=0&amp;showinfo=0";
                            break;
                        }
                    }
                }
                else
                {
                    var st = Link.Split(new string[] { "v=" }, StringSplitOptions.RemoveEmptyEntries);
                    this.Link = "https://www.youtube-nocookie.com/embed/" + st[1].Substring(0, 11) + "?rel=0&amp;showinfo=0";
                }
            }
            else { this.Link = Link; }
        }
        public void Connect()
        {
            try {
                 wvo.Source = (Uri)new WebBrowserUriTypeConverter().ConvertFromString(Link);
            } catch { }
        }
        public void Disconnect()
        {
            try { wvo.Source = (Uri)new WebBrowserUriTypeConverter().ConvertFromString(""); } catch { }
        }
    }
}
