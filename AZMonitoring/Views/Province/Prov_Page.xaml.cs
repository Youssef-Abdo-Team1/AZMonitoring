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
            TXTCAgent.Text = prov.CulturalAgentDGID.Name;
            TXTHCAdmin.Text = prov.HCAdministrationID.Name;
            TXTLAgent.Text = prov.LegalAgentDGID.Name;
            TXTSWelfare.Text = prov.SWelfareDID.Name;
        }

        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            statics.staticframe.Content = new Position_Person_Page(prov.HCAdministrationID.PositionID);
        }

        private void Grid_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            statics.staticframe.Content = new Position_Person_Page(prov.LegalAgentDGID.PositionID);
        }

        private void Grid_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {

            statics.staticframe.Content = new Position_Person_Page(prov.CulturalAgentDGID.PositionID);
        }

        private void Grid_MouseLeftButtonUp_3(object sender, MouseButtonEventArgs e)
        {
            statics.staticframe.Content = new Position_Person_Page(prov.SWelfareDID.PositionID);
        }
    }
}
