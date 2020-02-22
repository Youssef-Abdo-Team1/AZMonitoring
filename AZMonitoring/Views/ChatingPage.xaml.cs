using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Navigation;
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
    /// Interaction logic for ChatingPage.xaml
    /// </summary>
    public partial class ChatingPage : Page
    {
        DChat currentchat;
        public ChatingPage()
        {
            InitializeComponent();
        }

        public void newChatWindow(DChat chat)
        {
            currentchat = chat;
            //Initialize();
        }
        private void Initialize()
        {
            TXTName.Text = currentchat.Name;
            Img.ImageSource = null;
            LISTCurrentChatMessages.ItemsSource = currentchat.Messages;
            Img.ImageSource = currentchat.Image;
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if ((Keyboard.IsKeyDown(Key.RightShift) || Keyboard.IsKeyDown(Key.LeftShift)) && e.Key == Key.Enter)
            {
                e.Handled = true;
                SendMassage(TXTMassage.Text);
                TXTMassage.Text = "";
            }
        }

        private async void SendMassage(string text)
        {
            
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
