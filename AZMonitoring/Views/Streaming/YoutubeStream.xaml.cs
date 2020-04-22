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
        public YoutubeStream(string Link)
        {
            InitializeComponent();
            this.Link = Link;
        }
        public void Connect()
        {
            try { WB.Navigate(Link); } catch { }
        }
        public void Disconnect()
        {
            try { WB.NavigateToString(""); } catch { }
        }
    }
}
