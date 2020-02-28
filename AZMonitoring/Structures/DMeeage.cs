using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace AZMonitoring
{
    public class DMessage : Message, INotifyPropertyChanged
    {
        private ImageSource img;
        DAL.DAL DB = new DAL.DAL();
        public event PropertyChangedEventHandler PropertyChanged;
        public ImageSource Image
        {
            get
            {
                return img;
            }
            set
            {
                img = value;
                OnPropertyChanged("Image");
            }
        }
        protected void OnPropertyChanged(string name)
        {
            var handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        public string Text { get; set; }
        public string Link { get; set; }
        public HorizontalAlignment Alignment1 { get; set; }
        public HorizontalAlignment Alignment2 { get; set; }
        public SolidColorBrush Background { get ; set; }

        public void SetImage()
        {
            Task.Run(async () => { Image = await statics.DounloadImage(Content); });
        }

        internal void Initiate()
        {
            if (Type == MessageType.Text)
            {
                Text = Content;
            }
            else if (Type == MessageType.Broadcast)
            {

            }
            else if (Type == MessageType.File)
            {
                Text = "ملف للتحميل";
                Link = Content;
            }
            else if (Type == MessageType.Photo)
            {
                Text = "صورة";
                SetImage();
            }
            else if(Type == MessageType.VideoChat)
            {
                Text = "محادثة فيديو";
                OpenVideoChat(Content);
            }
            if (Sender == statics.LogedPerson.ID)
            {
                Background = Brushes.Teal;
                Alignment1 = HorizontalAlignment.Right;
                Alignment2 = HorizontalAlignment.Left;
            }
            else
            {
                Background = Brushes.Gainsboro;
                Alignment1 = HorizontalAlignment.Left;
                Alignment2 = HorizontalAlignment.Right;
            }

        }
        internal async void OpenVideoChat(string chat)
        {
            var ls = chat.Split('\n');
            if(await CheckOpenVideoChat())
            {
                Frame frame = new Frame();
                frame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
                frame.Height = 420;
                frame.Width = 640;

                var vd = new Views.VideoPages.VideoChatPage();
                var txt = new TextBox();
                vd.EnterChat(ls[0],ls[1]);
                txt.Text = $"{ls[0]}\n{ls[1]}";
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
                object result = await statics.myDH(stk);
            }
        }
        internal static DMessage GetDMessage(Message item)
        {
            var d = item as DMessage;
            if(d != null)
            {

            }
            else
            {
                d = new DMessage();
                d.ID = item.ID;
                d.Content = item.Content;
                d.Date = item.Date;
                d.Sender = item.Sender;
                d.Read = item.Read;
                d.Type = item.Type;
            }
            d.Initiate();
            return d;
        }
        internal async Task<bool> CheckOpenVideoChat()
        {
            TextBlock txt1 = new TextBlock();
            txt1.HorizontalAlignment = HorizontalAlignment.Center;
            txt1.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF53B3B"));
            txt1.Margin = new Thickness(4);
            txt1.TextWrapping = TextWrapping.WrapWithOverflow;
            txt1.FontSize = 18;
            txt1.Text = "هناك محادثة فيديو قادمة .. هل تريد الرد عليها ؟";

            Button btn1 = new Button();
            Style style = Application.Current.FindResource("MaterialDesignFlatButton") as Style;
            btn1.Style = style;
            btn1.Width = 115;
            btn1.Height = 30;
            btn1.Margin = new Thickness(5);
            btn1.Command = MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand;
            btn1.CommandParameter = true;
            btn1.Content = "نعم";

            Button btn2 = new Button();
            Style style2 = Application.Current.FindResource("MaterialDesignFlatButton") as Style;
            btn2.Style = style2;
            btn2.Width = 115;
            btn2.Height = 30;
            btn2.Margin = new Thickness(5);
            btn2.Command = MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand;
            btn2.CommandParameter = false;
            btn2.Content = "لا";


            DockPanel dck = new DockPanel();
            dck.Children.Add(btn1);
            dck.Children.Add(btn2);

            StackPanel stk = new StackPanel();
            stk.Width = 250;
            stk.Children.Add(txt1);
            stk.Children.Add(dck);
            object result = await statics.myDH(stk);
            if (result is bool boolResult && boolResult)
            {
                return true;
            }
            return false;
        }
    }
}