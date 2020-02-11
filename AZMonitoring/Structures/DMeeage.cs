using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace AZMonitoring
{
    public class DMessage : Message
    {
        public string Text { get; set; }
        public HorizontalAlignment Alignment1 { get; set; }
        public HorizontalAlignment Alignment2 { get; set; }
        public SolidColorBrush Background { get ; set; }
        public ImageSource Image { get; set; }

        public void Set()
        {
            throw new System.NotImplementedException();
        }

        public async void InitializeImage()
        {

        }
    }
}