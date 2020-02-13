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
    /// Interaction logic for Prov_Page.xaml
    /// </summary>
    public partial class Prov_Page : Page
    {
        Province prov;
        public Prov_Page(Province prov)
        {
            InitializeComponent();
            this.prov = prov;
            InitializeFields();
        }
        async void InitializeFields()
        {
            GBGA.Header = $"الإدارة المركزية لمحافظة {prov.Name}";
            GBA.Header = $"الادارات التعليمية لمحافظة {prov.Name}";
            GBI.Header = $"توجيه محافظة {prov.Name}";
            ListGAdmin.ItemsSource = testlist;
            ListGAdmin.Items.Refresh();
        }
    }
}
