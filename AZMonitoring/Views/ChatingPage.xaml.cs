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
    /// Interaction logic for ChatingPage.xaml
    /// </summary>
    public partial class ChatingPage : Page
    {
        DChat currentchat;
        string userid;
        public ChatingPage()
        {
            InitializeComponent();
        }

        public void newChatWindow(DChat chat, string userid)
        {
            currentchat = chat;
            this.userid = userid;
            Initialize();
        }
        private async void Initialize()
        {
            TXTName.Text = currentchat.Name;
            Img.ImageSource = null;
            LISTCurrentChatMessages.ItemsSource = currentchat.Messages;
            await Task.Run(() => { currentchat.InitializeMessages(userid); });
            await Task.Run(() => { currentchat.SetImage(); });
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
    }
}
