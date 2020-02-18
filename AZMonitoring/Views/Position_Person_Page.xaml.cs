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
    /// Interaction logic for Position_Person_Page.xaml
    /// </summary>
    public partial class Position_Person_Page : Page
    {
        public Position_Person_Page(string positionID)
        {
            InitializeComponent();
            TXTtest.Text = positionID;
        }

        private void FrameBack(object sender, MouseButtonEventArgs e) { try { statics.staticframe.GoBack(); } catch { } }
    }
}
