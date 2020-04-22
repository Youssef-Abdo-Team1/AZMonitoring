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
        GInstruct gInstruct { get; set; }
        public GinstructPage(GInstruct gInstruct)
        {
            InitializeComponent();
            gInstruct.HeadsID[0].PositionID = "مسؤل الانشطة والمسابقات";
            gInstruct.HeadsID[1].PositionID = "مسؤل اللياقة البدنية";
            gInstruct.HeadsID[2].PositionID = "مسؤل الدعم المالي";

            this.gInstruct = gInstruct;
            this.DataContext = gInstruct;
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            statics.staticframe.Content = new Position_Person_Page(gInstruct.GeneralInstructorID.PositionID);
        }

        private void Border_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            statics.staticframe.Content = new Position_Person_Page(gInstruct.FirstInstructorID.PositionID);
        }

        private void Card_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void ItemsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ItemsList.SelectedIndex == 0)
            {
                var p = new Position_Person_Page("-M1ZwTRDzU9ld2Wsx0W_"); ;
                statics.staticframe.Content = p;
                p.WorkFrame.Content = new work.WorksPage(new List<Work> { new Work { Name = "كرة قدم" },
                    new Work { Name = "كرة طائرة" },
                    new Work { Name = "كرة يد"},
                    new Work { Name = "كرة سالة"},
                    new Work { Name = "جمباز"},
                    new Work { Name = "العاب قوي"},
                    new Work { Name = "تنس طاولة"},
                    new Work { Name = "اختراق ضاحية"},
                    new Work { Name = "سباحة"},
                    new Work { Name = "دراجات"},
                    new Work { Name = "شطرانج"},
                    new Work { Name = "كراتيه"}
                });

            }
            else if (ItemsList.SelectedIndex == 1)
            {
                statics.staticframe.Content = new Position_Person_Page("-M1ZwTs5n0zt2nlXJk9S");
            }
            else if (ItemsList.SelectedIndex == 2)
            {
                statics.staticframe.Content = new Position_Person_Page("- M1ZwUV6qFJb6bs11N1g");
            }
        }
    }
}
