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

namespace AZMonitoring.Views.intruct
{
    /// <summary>
    /// Interaction logic for InstructPage.xaml
    /// </summary>
    public partial class InstructPage : Page
    {
        Instruct Instruct { get; set; }
        public InstructPage()
        {
            InitializeComponent();
        }
        internal void Initialize(Instruct instruct)
        {
            Instruct = instruct;
            TXT.Text = Instruct.ID;
        }
    }
}
