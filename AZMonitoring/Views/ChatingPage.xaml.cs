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
        DAL.DAL DB = new DAL.DAL();
        MainWindow main;
        public ChatingPage(MainWindow main)
        {
            InitializeComponent();
            this.main = main;
            statics.MessageRefreshDelegate = () => {
                Dispatcher.Invoke(() =>
                {
                    MessageScroller.ScrollToEnd();
                    LISTCurrentChatMessages.Items.Refresh();
                });
            };
        }

        public void newChatWindow(DChat chat)
        {
            statics.CurrentChat = chat;
            DB.ClearMessageLisner();
            DB.SetMessagesListener(chat.ID);
            Initialize();
        }
        private void Initialize()
        {
            TXTName.Text = statics.CurrentChat.Name;
            Img.ImageSource = null;
            LISTCurrentChatMessages.ItemsSource = statics.CurrentChat.Messages;
            Img.ImageSource = statics.CurrentChat.Image;
            LISTCurrentChatMessages.Items.Refresh();
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if ((Keyboard.IsKeyDown(Key.RightShift) || Keyboard.IsKeyDown(Key.LeftShift)) && e.Key == Key.Enter)
            {
                string st = TXTMassage.Text;
                int s = TXTMassage.SelectionStart;
                string s1 = st.Substring(0, s);
                string s2 = st.Substring(s, st.Length - s);
                s++;
                TXTMassage.Text = s1 + "\n" + s2;
                TXTMassage.SelectionStart = s;
            }
            else if (e.Key == Key.Enter)
            {
                e.Handled = true;
                if(TXTMassage.Text.Replace("\n","").Replace(" ","").Length > 0) { SendMassage(TXTMassage.Text); }
                TXTMassage.Text = "";
            }
        }

        private void SendMassage(string text)
        {
            try { DB.AddMessage(statics.CurrentChat, new Message { Date = DateTime.Now, Content = text, Read = false, Type = MessageType.Text, Sender = statics.LogedPerson.ID }); }
            catch { }
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void BTNSend_Click(object sender, RoutedEventArgs e)
        {
            if (TXTMassage.Text.Replace("\n", "").Replace(" ", "").Length > 0) { SendMassage(TXTMassage.Text); }
            TXTMassage.Text = "";
        }

        private void PackIcon_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            main.OpenVideoChat(statics.CurrentChat.IDPerson1 == statics.LogedPerson.ID ? statics.CurrentChat.IDPerson2 : statics.CurrentChat.IDPerson1);
            main.OCFrame(false);
        }

        private void BTNSendPH_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
