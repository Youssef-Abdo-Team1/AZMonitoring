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
    /// <summary>
    /// Interaction logic for SysManageMainPage.xaml
    /// </summary>
    public partial class SysManageMainPage : Page
    {
        List<StPages> Pages = new List<StPages>();
        public SysManageMainPage()
        {
            InitializeComponent();
            InitializeFields();
        }
        void InitializeFields()
        {
            Pages.Add(new StPages { Page = new Views.SysManage.Prov_manage_Page(), Header = "صفحة إدارة المحافظات" });
            PagesListView.ItemsSource = Pages;
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
