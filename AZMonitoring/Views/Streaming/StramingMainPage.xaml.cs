using AZMonitoring.Structures.Pages;
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
    public class StSPages
    {
        public YoutubeStream youtube { get; set; }
        public string Header { get; set; }
    }
    /// <summary>
    /// Interaction logic for StramingMainPage.xaml
    /// </summary>
    public partial class StramingMainPage : Page
    {
        List<StSPages> youtubes { get; set; }
        public StramingMainPage()
        {
            InitializeComponent();
            InitializeFields();
        }
        void InitializeFields()
        {
            youtubes = new List<StSPages>();
            youtubes.Add(new StSPages {youtube = new YoutubeStream("https://www.youtube.com/watch?v=50PGoDDnGwA&list=RDCMUC1FYFqgdW_1LueJTmXn-8cg&start_radio=1&t=187", "YoutubeLink"), Header = "سور الكهف" });
            youtubes.Add(new StSPages { youtube = new YoutubeStream("EEIk7gwjgIM", "YoutubeID"), Header = "سورة الكهف المنشاوي" });
            youtubes.Add(new StSPages { youtube = new YoutubeStream("https://youtu.be/3pA6ezncBGo/sadss", "YoutubeLink") , Header = "الشيخ الشعراوي معجزات الاسراء والمعراج"});
            PagesListView.ItemsSource = youtubes;
            PagesListView.Items.Refresh();
            PagesListView.SelectedIndex = 0;
        }
        void StreamOffAllPages()
        {
            foreach (var item in youtubes)
            {
                item.youtube.Disconnect();
            }
        }
        private void PagesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PagesListView.SelectedItem != null)
            {
                StreamOffAllPages();
                ContinerFrame.Content = ((StSPages)PagesListView.SelectedItem).youtube;
                ((StSPages)PagesListView.SelectedItem).youtube.Connect();
            }
        }
    }
}
