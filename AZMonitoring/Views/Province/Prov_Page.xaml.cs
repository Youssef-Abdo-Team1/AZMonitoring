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
        List<GInstruct> GInstructions;
        List<AZMonitoring.Administration> administrations;
        DAL.DAL DB = new DAL.DAL();
        Administration.AdministrationPage administrationPage = new Administration.AdministrationPage();
        public Prov_Page()
        {
            InitializeComponent();
        }
        private async void InitializeAdministrations()
        {
            administrations = new List<AZMonitoring.Administration>();
            if (prov.AdministrationsID != null && prov.AdministrationsID.Count > 0)
            {
                administrations.AddRange(await DB.GetAdministrationsAsync(prov.AdministrationsID));
            }
            LVAdministrations.ItemsSource = administrations;
            LVAdministrations.Items.Refresh();
        }
        private async void InitializeGInstructs()
        {
            GInstructions = null;
            if (prov.GInstructsID != null && prov.GInstructsID.Count > 0)
            {
                GInstructions = await DB.GetGInstructs(prov.GInstructsID);
            }
            LVInstructs.ItemsSource = GInstructions;
            LVInstructs.Items.Refresh();
        }
        internal void InitializeFields(Province prov)
        {
            this.prov = prov;
            this.DataContext = prov;
            InitializeGInstructs();
            InitializeAdministrations();
            GBGA.Header = $"الإدارة المركزية لمنطقة {prov.Name} الازهرية";
            GBA.Header = $"الادارات التعليمية بممنطقة  {prov.Name} الازهرية";
            GBI.Header = $"التوجيه والمتابعة بمنطقة {prov.Name} الازهرية";
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

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            administrationPage.Initialize_Administration((AZMonitoring.Administration)LVAdministrations.SelectedItem);
            statics.staticframe.Content = administrationPage;
        }

        private void ListViewItem_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            
        }
    }
}
