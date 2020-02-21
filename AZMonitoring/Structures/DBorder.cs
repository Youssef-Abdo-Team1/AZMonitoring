using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AZMonitoring.Structures
{
    class DBorderAD<T> : Border
    {
        internal Button button { get; set; }
        public DBorderAD(T t)
        {
            //border
            Height = 100;
            Width = 100;
            Background = statics.GetBrushFromString("#88000000");
            Margin = new Thickness(10);
            BorderBrush = Brushes.White;
            BorderThickness = new Thickness(1);

            //textblock
            var txt = new TextBlock();
            txt.Text = t.ToString();
            txt.TextWrapping = TextWrapping.WrapWithOverflow;
            txt.HorizontalAlignment = HorizontalAlignment.Center;
            txt.VerticalAlignment = VerticalAlignment.Center;

            //button
            var btn = new Button();
            btn.HorizontalAlignment = HorizontalAlignment.Right;
            btn.VerticalAlignment = VerticalAlignment.Top;
            btn.Content = "X";
            btn.ToolTip = "حذف";
            button = btn;

            //grid
            var gr = new Grid();
            gr.Children.Add(txt);
            gr.Children.Add(btn);
            base.Child = gr;
        }
    }
}
