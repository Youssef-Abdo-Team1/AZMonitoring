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
    public class DMessage : Message
    {
        public string Text { get; set; }
        public string Link { get; set; }
        public string Photo { get; set; }
        public HorizontalAlignment Alignment1 { get; set; }
        public HorizontalAlignment Alignment2 { get; set; }
        public SolidColorBrush Background { get ; set; }

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
                Photo = Content;
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
            var d = new DMessage { Content = item.Content, Date = item.Date, Sender = item.Sender, Read = item.Read, Type = item.Type };
            d.Initiate();
            return d;
        }
    }
}