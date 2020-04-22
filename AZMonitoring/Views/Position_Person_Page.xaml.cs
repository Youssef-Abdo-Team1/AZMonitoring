using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
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
        public BitmapImage pickerImage;
        private FileInfo SelectedFile { get; set; }
        Person person { get; set; }
        DAL.DAL DB = new DAL.DAL();
        public Position_Person_Page(string positionID)
        {
            InitializeComponent();
            initiate(positionID);
        }
        async void initiate(string p)
        {
            person = await DB.GetPersonbyPositionID(p);
            DataContext = person;
        }
        public Position_Person_Page(Person person, bool edit = false)
        {
            InitializeComponent();
            this.DataContext = person;
        }
        internal async void Initiate(string personid)
        {
            Person person = await DB.GetPersonbyID(personid);
            this.DataContext = person;
        }

        private void Edite_Profile(object sender, MouseButtonEventArgs e)
        {
            Edite.Visibility = Visibility.Hidden;
            Name.IsReadOnly = false;
            UserNumber.IsReadOnly = false;
            OldPassword.Visibility = Visibility.Visible;
            NewPassword.Visibility = Visibility.Visible;
            SaveBtn.Visibility = Visibility.Visible;
            UploadImage.Visibility = Visibility.Visible;
        }

        private void UploadImage_Click(object sender, RoutedEventArgs e)
        {
            var picker = new OpenFileDialog
            {
                Filter = "Images (*.BMP;*.JPG;*.GIF,*.PNG,*.TIFF)|*.BMP;*.JPG;*.GIF;*.PNG;*.TIFF"
            };
            if (picker.ShowDialog() == DialogResult.OK)
            {
                //System.Windows.MessageBox.Show(picker.FileName);
                pickerImage = new BitmapImage(new Uri(picker.FileName));
                img.ImageSource = pickerImage;
            }
        }

        private void SaveBtn_Click(object sender, MouseButtonEventArgs e)
        {
            //save in data base
        }
        private void FrameBack(object sender, MouseButtonEventArgs e) { try { statics.staticframe.GoBack(); } catch { } }
    }
}
