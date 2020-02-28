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
        List<Administration> administrations;
        DAL.DAL DB = new DAL.DAL();
        public Prov_Page(Province prov)
        {
            InitializeComponent();
            this.prov = prov;
            InitializeFields();
        }
        private async void InitializeAdministrations()
        {
            administrations = new List<Administration>();
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

        void InitializeFields()
        {
            InitializeGInstructs();
            InitializeAdministrations();
            GBGA.Header = $"الإدارة المركزية لمنطقة {prov.Name} الازهرية";
            GBA.Header = $"الادارات التعليمية بممنطقة  {prov.Name} الازهرية";
            GBI.Header = $"التوجيه والمتابعة بمنطقة {prov.Name} الازهرية";
            TXTCAgent.Text = prov.CulturalAgentDGID.Name;
            TXTHCAdmin.Text = prov.HCAdministrationID.Name;
            TXTLAgent.Text = prov.LegalAgentDGID.Name;
            TXTSWelfare.Text = prov.SWelfareDID.Name;
            IMGCAG.ImageSource = IMGHCA.ImageSource = IMGLAG.ImageSource = IMGSW.ImageSource = null;
            i1(); i2(); i3(); i4();
        }
        async void i1() { IMGHCA.ImageSource = await statics.DounloadImage(prov.HCAdministrationID.Photo); }
        async void i2() { IMGCAG.ImageSource = await statics.DounloadImage(prov.CulturalAgentDGID.Photo); }
        async void i3() { IMGLAG.ImageSource = await statics.DounloadImage(prov.LegalAgentDGID.Photo); }
        async void i4() { IMGSW.ImageSource = await statics.DounloadImage(prov.SWelfareDID.Photo); }


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
