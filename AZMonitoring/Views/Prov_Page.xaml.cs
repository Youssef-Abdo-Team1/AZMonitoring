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
        public Prov_Page()
        {
            InitializeComponent();
            var testlist = new List<TestClass>();
            testlist.Add(new TestClass { Image = null,PositionName = "اي حاجة", PersonName = "اي بتنجان" });
            testlist.Add(new TestClass { Image = null, PositionName = "اي حاجة", PersonName = "اي بتنجان" });
            testlist.Add(new TestClass { Image = null, PositionName = "اي حاجة", PersonName = "اي بتنجان" });
            testlist.Add(new TestClass { Image = null, PositionName = "اي حاجة", PersonName = "اي بتنجان" });
            ListGAdmin.ItemsSource = testlist;
            ListGAdmin.Items.Refresh();
        }
    }

    internal class TestClass
    {
        public ImageSource Image { get; set; }
        public string PositionName { get; set; }
        public string PersonName { get; set; }
    }
}
