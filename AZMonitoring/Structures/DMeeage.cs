using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

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

        public DMessage()
        {
            
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

        internal static DMessage GetDMessage(Message item)
        {
            var d = item as DMessage;
            if(d != null)
            {

            }
            else
            {
                d.ID = item.ID;
                d.Content = item.Content;
                d.Date = item.Date;
                d.Sender = item.Sender;
                d.Read = item.Read;
                d.Type = item.Type;
            }
            return d;
        }
    }
}