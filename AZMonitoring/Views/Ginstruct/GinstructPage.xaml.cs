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
            this.DataContext = new GInstruct
            {
                FirstInstructorID = new StaticInfo
                {
                    Name = "Abdo mostafa",
                    Photo = null
                },
                GeneralInstructorID = new StaticInfo
                {
                    Name = "Youssef",
                    Photo = null
                },
                Name = "توجيه التربيه الرياضيه",
            };
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Border_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {

        }
        public void InitializeFields(GInstruct gInstruct)
        {
            this.DataContext = gInstruct;
        }
    }
}
