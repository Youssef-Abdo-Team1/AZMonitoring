﻿using System;
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

namespace AZMonitoring.Views.instituation
{
    /// <summary>
    /// Interaction logic for InstitutionPage.xaml
    /// </summary>
    public partial class InstitutionPage : Page
    {
        Institution Institution { get; set; }
        public InstitutionPage(Institution institution)
        {
            InitializeComponent();

            this.Institution = institution;
            DataContext = this.Institution;
            ini();
        }
        async void ini()
        {
            TXTTeacher.Text = "محمد رأفت";
            TXTSH.Text = "سحر عبد الله";
        }
    }
}
