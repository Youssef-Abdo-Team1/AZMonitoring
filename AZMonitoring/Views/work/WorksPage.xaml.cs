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

namespace AZMonitoring.Views.work
{
    /// <summary>
    /// Interaction logic for WorksPage.xaml
    /// </summary>
    public partial class WorksPage : Page
    {
        public WorksPage(List<Work> works)
        {
            InitializeComponent();
            LVInstructs.ItemsSource = works;
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
