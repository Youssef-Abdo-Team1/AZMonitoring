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

namespace AZMonitoring.Views.Ginstruct
{
    /// <summary>
    /// Interaction logic for GinstructPage.xaml
    /// </summary>
    public partial class GinstructPage : Page
    {
        public GinstructPage()
        {
            InitializeComponent();
            InitializeFields();
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Border_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {

        }
        async void InitializeFields()
        {
            //statics.currentgInstruct =
            //  IMGCAG.ImageSource = IMGHCA.ImageSource = IMGLAG.ImageSource = IMGSW.ImageSource = null;
            //IMGHCA.ImageSource = await statics.DounloadImage(prov.HCAdministrationID.Photo);
            //IMGCAG.ImageSource = await statics.DounloadImage(prov.CulturalAgentDGID.Photo);
            //IMGLAG.ImageSource = await statics.DounloadImage(prov.LegalAgentDGID.Photo);
            //IMGSW.ImageSource = await statics.DounloadImage(prov.SWelfareDID.Photo);
        }
    }
}
