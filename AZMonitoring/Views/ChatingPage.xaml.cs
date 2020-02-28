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
                e.Handled = true;
                SendMassage(TXTMassage.Text);
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
            SendMassage(TXTMassage.Text);
            TXTMassage.Text = "";
        }

        private async void PackIcon_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Frame frame = new Frame();
            frame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            frame.Height = 420;
            frame.Width = 640;

            var vd = new Views.VideoPages.VideoChatPage();
            var txt = new TextBox();
            var ls = await vd.Createvideochat();
            txt.Text = $"{ls[0]}\n{ls[1]}";
            try
            { DB.AddMessage(statics.CurrentChat, new Message { Date = DateTime.Now, Content = $"{ls[0]}\n{ls[1]}", Read = false, Type = MessageType.VideoChat, Sender = statics.LogedPerson.ID }); } catch { }
            frame.Content = vd;

            Button btn2 = new Button();
            Style style2 = Application.Current.FindResource("MaterialDesignFlatButton") as Style;
            btn2.Click += (s, ee) => { vd.disconnect(); };
            btn2.Style = style2;
            btn2.Width = 115;
            btn2.Height = 30;
            btn2.Margin = new Thickness(5);
            btn2.Command = MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand;
            btn2.CommandParameter = false;
            btn2.Content = "إغلاق";


            StackPanel stk = new StackPanel();
            stk.Children.Add(frame);
            stk.Children.Add(btn2);
            stk.Children.Add(txt);
            object result = await main.OpenDialogHost(stk);
        }
    }
}
