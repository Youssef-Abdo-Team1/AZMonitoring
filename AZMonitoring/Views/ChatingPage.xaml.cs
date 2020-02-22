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
        public ChatingPage()
        {
            InitializeComponent();
            statics.MessageRefreshDelegate = () => {
                Dispatcher.Invoke(() =>
                {
                    MessageScroller.ScrollToEnd();
                    LISTCurrentChatMessages.Items.Refresh();
                });
            };
            //var ms = new List<DMessage>();
            //var x1 = new DMessage { Content = "hello word!!", Type = MessageType.Text, Sender = "66" };
            //x1.Initiate();
            //ms.Add(x1);
            //var x2 = new DMessage { Content = @"https://firebasestorage.googleapis.com/v0/b/fir-test1-fb35d.appspot.com/o/FriendlyChat%2FUsers%2Ftest.jpg?alt=media&token=a4200143-fc2f-4ee7-923e-1d59f1afcd6a", Type = MessageType.Photo, Sender = "dsfdsf" };
            //x2.Initiate();
            //ms.Add(x2);
            //var x3 = new DMessage { Content = "hello !!", Type = MessageType.Text, Sender = "66" };
            //x3.Initiate();
            //ms.Add(x3);
            //var x4 = new DMessage { Content = @"https://firebasestorage.googleapis.com/v0/b/fir-test1-fb35d.appspot.com/o/%D8%A7%D8%B3%D9%85%D8%A7%D8%A1%20%D9%84%D9%84%D8%A8%D8%B1%D8%A7%D9%85%D8%AC.docx?alt=media&token=ae37fded-2a7e-4b33-9e1e-075be6b56806", Type = MessageType.File, Sender = "dsfdsf" };
            //x4.Initiate();
            //ms.Add(x4);
            //LISTCurrentChatMessages.ItemsSource = ms;
            //LISTCurrentChatMessages.Items.Refresh();
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
                e.Handled = true;
                SendMassage(TXTMassage.Text);
                TXTMassage.Text = "";
            }
        }

        private void SendMassage(string text)
        {
            DB.AddMessage(statics.CurrentChat.ID, new Message { Date = DateTime.Now, Content = text, Read = false, Type = MessageType.Text, Sender = statics.LogedPerson.ID });
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void BTNSend_Click(object sender, RoutedEventArgs e)
        {
            SendMassage(TXTMassage.Text);
            TXTMassage.Text = "";
        }
    }
}
