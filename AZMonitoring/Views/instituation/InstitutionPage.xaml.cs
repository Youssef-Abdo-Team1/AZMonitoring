using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace AZMonitoring.Views.instituation
{
    class DInstitution :INotifyPropertyChanged
    {
        string shiekhname,shiekhphoto;
        public Institution Institution { get; private set; }
        public string ShiekhPhoto { get { return shiekhname; } 
            set {
                shiekhname = value;
                OnPropertyRaised("ShiekhPhoto");
            } }
        public string ShiekhName {
            get { return shiekhphoto; }
            set { shiekhphoto = value; OnPropertyRaised("ShiekhName"); }
        }
        public List<string> MyProperty { get; set; }
        public DInstitution(Institution institution)
        {
            Institution = institution;
            Initialize();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        async void Initialize()
        {
            ShiekhName = await statics.DB.GetPersonName(Institution.SheikhID);
            ShiekhPhoto = await statics.DB.GetPersonPhoto(Institution.SheikhID);
        }
    }
    /// <summary>
    /// Interaction logic for InstitutionPage.xaml
    /// </summary>
    public partial class InstitutionPage : Page
    {
        DInstitution Institution { get; set; }
        public InstitutionPage()
        {
            InitializeComponent();
        }
        public async void ini(Institution institution)
        {
            DataContext = null;
            Institution = new DInstitution(institution);
            DataContext = Institution;
        }

        private void Card_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
