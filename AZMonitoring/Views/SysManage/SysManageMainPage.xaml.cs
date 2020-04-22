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

namespace AZMonitoring.Views.SysManage
{
    public class StSPages
    {
        public Views.Streaming.YoutubeStream youtube { get; set; }
        public string Header { get; set; }
    }
    /// <summary>
    /// Interaction logic for SysManageMainPage.xaml
    /// </summary>
    public partial class SysManageMainPage : Page
    {
        public SysManageMainPage()
        {
            InitializeComponent();
            InitializeFields();
        }
        void InitializeFields()
        {
            PagesListView.ItemsSource = statics.Data_Mang_Pages;
            PagesListView.Items.Refresh();
            PagesListView.SelectedIndex = 0;
        }

        private void PagesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(PagesListView.SelectedItem != null)
            {
                ContinerFrame.Content = ((StPages)PagesListView.SelectedItem).Page;
            }
        }
    }
}
