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

namespace AZMonitoring.Views
{
    /// <summary>
    /// Interaction logic for All_Chats_Page.xaml
    /// </summary>
    public partial class All_Chats_Page : Page
    {
        MainWindow Main;
        public All_Chats_Page(MainWindow main)
        {
            InitializeComponent();
            Main = main;
        }
        internal void Refresh()
        {
            LVChats.ItemsSource = statics.DChats;
            LVChats.Items.Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ClickCount == 2 && LVChats.SelectedItem != null)
            {
                Main.OpenChatFrame((DChat)LVChats.SelectedItem);
            }
        }
    }
}
