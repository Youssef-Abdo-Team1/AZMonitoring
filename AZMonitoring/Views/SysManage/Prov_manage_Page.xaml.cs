using AZMonitoring.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using static AZMonitoring.statics;

namespace AZMonitoring.Views.SysManage
{
    /// <summary>
    /// Interaction logic for Prov_manage_Page.xaml
    /// </summary>
    public partial class Prov_manage_Page : Page
    {
        Person hca,lag,cag,wm;
        public Prov_manage_Page()
        {
            InitializeComponent();
            reset();
        }
        void reset()
        {
            GBAE.Visibility = CBChProv.Visibility = TXTChprove.Visibility = BTNSave.Visibility = Visibility.Hidden;
            TXTCAG.Text = TXTDes.Text = TXTHCA.Text = TXTLAG.Text = TXTName.Text = TXTWM.Text = "";
            CBChProv.SelectedIndex = -1;
            hca = lag = cag = wm = null;
            CBChProv.ItemsSource = statics.Provinces;
            CBChProv.Items.Refresh();
        }
        private void BTNSave_Click(object sender, RoutedEventArgs e)
        {
            IsEnabled = false;
            if (CBSAED.SelectedIndex == 0)
            {
                try
                {
                    if(MessageBox.Show("هل انت تريد اضافة محافظة جديدة ؟", "اضافة محافظة", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.RightAlign) == MessageBoxResult.Yes)
                    {
                        if (hca != null && cag != null && lag != null && wm != null)
                        {
                            DB.AddProvince(new Province { Name = TXTName.Text, Description = TXTDes.Text }, hca, lag, cag, wm).Start();
                        }
                        else
                        {
                            MessageBox.Show("هناك خطأ في احد اعضاء الادارة المركزية الرجاء إعادة ادخال رقم السجل و اضغط Enter", "اضافة محافظة", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.RightAlign);
                        }
                    }
                }
                catch { }
            }
            else if (CBSAED.SelectedIndex == 1)
            {
                try
                {
                    if (MessageBox.Show("هل انت تريد حفظ التعديلات ؟", "حفظ التعديلات", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.RightAlign) == MessageBoxResult.Yes)
                    {
                        if (hca != null && cag != null && lag != null && wm != null)
                        {
                            var p = (Province)CBChProv.SelectedItem;
                            p.Name = TXTName.Text;
                            p.Description = TXTDes.Text;
                            DB.UpdateProvince(p, hca, lag, cag, wm).Start();
                        }
                        else
                        {
                            MessageBox.Show("هناك خطأ في احد اعضاء الادارة المركزية الرجاء إعادة ادخال رقم السجل و اضغط Enter", "تعديل محافظة", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.RightAlign);
                        }
                    }
                }
                catch { }
            }
            else if (CBSAED.SelectedIndex == 2)
            {
                try
                {
                    if (MessageBox.Show("هل انت تريد حذف المحافظة ؟", "حذف محافظة", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes, MessageBoxOptions.RightAlign) == MessageBoxResult.Yes)
                    {
                        DB.DeleteProvincebyID(((Province)CBChProv.SelectedItem).Name).Start();
                    }
                }
                catch { }
            }
            CBSAED.SelectedIndex = -1;
            reset();
            IsEnabled = true;
        }
        private async void CBChProv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(CBChProv.SelectedItem != null)
            {
                if(CBSAED.SelectedIndex == 1)
                {
                    try
                    {
                        TXTGetingData.Visibility = Visibility.Visible;
                        GBAE.Visibility = BTNSave.Visibility = Visibility.Hidden;
                        var p = (Province)CBChProv.SelectedItem;
                        GBAE.Header = $"تعديل بيانات محافظة {p.Name}";
                        BTNSave.Content = $"حفظ التعديلات لمحافظة {p.Name}";
                        TXTName.Text = p.Name;
                        TXTDes.Text = p.Description;
                        hca = await DB.GetPersonbyPositionID(p.HCAdministrationID.PositionID);
                        cag = await DB.GetPersonbyPositionID(p.CulturalAgentDGID.PositionID);
                        lag = await DB.GetPersonbyPositionID(p.LegalAgentDGID.PositionID);
                        wm = await DB.GetPersonbyPositionID(p.SWelfareDID.PositionID);
                        TXTCAG.Text = cag.Name;
                        TXTHCA.Text = hca.Name;
                        TXTLAG.Text = lag.Name;
                        TXTWM.Text = wm.Name;
                        TXTGetingData.Visibility = Visibility.Hidden;
                        BTNSave.Visibility = GBAE.Visibility = Visibility;
                    }
                    catch { }
                }
                else if (CBSAED.SelectedIndex == 2)
                {
                    try
                    {
                        BTNSave.Content = $"حذف المحافظة";
                        BTNSave.Visibility = Visibility.Visible;
                    }
                    catch { }
                }
                else
                {
                    reset();
                }
            }
        }
        private void CBSAED_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(CBSAED.SelectedIndex == 0)
            {
                reset();
                BTNSave.Visibility = GBAE.Visibility = Visibility;
                GBAE.Header = "اضافة محافظة جديدة";
                BTNSave.Content = "اضافة المحافظة وحفظ";
            }
            else if (CBSAED.SelectedIndex == 1)
            {
                reset();
                CBChProv.Visibility = TXTChprove.Visibility = Visibility.Visible;
            }
            else if (CBSAED.SelectedIndex == 2)
            {
                reset();
                CBChProv.Visibility = TXTChprove.Visibility = Visibility.Visible;
            }
            else
            {
                reset();
            }
        }
        private async void TXTHCA_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                try
                {
                    hca = await DB.GetPersonbyID(TXTHCA.Text);
                    TXTHCA.Text = hca.Name;
                }
                catch { TXTHCA.Text = "الشخص غير موجود او أن هناك مشكلة في جلب البيانات"; }
            }
        }
        private async void TXTCAG_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    cag = await DB.GetPersonbyID(TXTCAG.Text);
                    TXTCAG.Text = cag.Name;
                }
                catch { TXTCAG.Text = "الشخص غير موجود او أن هناك مشكلة في جلب البيانات"; }
            }
        }
        private async void TXTWM_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    wm = await DB.GetPersonbyID(TXTWM.Text);
                    TXTWM.Text = wm.Name;
                }
                catch { TXTWM.Text = "الشخص غير موجود او أن هناك مشكلة في جلب البيانات"; }
            }
        }
        private async void TXTLAG_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    lag = await DB.GetPersonbyID(TXTLAG.Text);
                    TXTLAG.Text = lag.Name;
                }
                catch { TXTLAG.Text = "الشخص غير موجود او أن هناك مشكلة في جلب البيانات"; }
            }
        }
    }
}
