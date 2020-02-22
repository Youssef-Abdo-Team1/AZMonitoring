using AZMonitoring.Structures.Pages;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AZMonitoring
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool fro = false,ch = false,chts = false,ocprovlist = false;
        DPerson LogedPerson;
        public readonly Views.ChatingPage chatingPage = new Views.ChatingPage();
        private DAL.DAL DB;
        public MainWindow()
        {
            InitializeComponent();
            DB = new DAL.DAL();
            DB.CreateConnection(this);
            statics.staticframe = MainFrameContainer;
            //DB.test_addChats();
            //DB.Test_addpersons();
            //DB.Test_add_positions();
            //DB.Test_addProvinces();
            DB.test_add();
            MainFrameContainer.Content = new Views.Dashboard.Dashboard_MainPage();

        }
        internal void Initialize_Data_Manage_Pages()
        {
            statics.Data_Mang_Pages = new List<StPages>();
            //int x = statics.LogedPersonPosition.Level;
            /*صلاحيات المدير للأشراف علي مستوي الجمهورية*/ statics.Data_Mang_Pages.Add(new StPages { Page = new Views.SysManage.Prov_manage_Page(), Header = "صفحة إدارة المحافظات" });
            //if (x < 2) { /* صلاحيات مدير الادارة المركزية*/ }
            //if (x < 3) { /* صلاحيات الوكيل الثقافي - الوكيل الشرعي - مدير رعاية الطلاب*/ }
            //if (x < 4) { /* صلاحيات الموجه العام - الموجه الاول*/ }
            //if (x < 5) { /* صلاحيات مسؤلين التوجيه العام*/ }
            //if (x < 6) { /* صلاحيات الموجهين التابعين للادارت التعليمية*/ }
            //if (x < 7) { /* صلاحيات شيخ المعهد*/ }
            //if (x < 8) { /* صلاحيات المعلم في المفهد*/ }

        }
        internal async void Initialize_Prov_Control_List()
        {
            if (statics.LogedPersonPosition != null)
            {
                await Dispatcher.InvokeAsync(async () => {
                    statics.Provinces = new List<Province>();
                    if (statics.LogedPersonPosition.Level == 0)
                    {
                        statics.Provinces.AddRange(await DB.GetAllProvinces());
                }
                    else { statics.Provinces.Add(await DB.GetProvincebyName(statics.LogedPersonPosition.IDProvince)); }
                MainListViewProv.ItemsSource = statics.Provinces;
                    MainListViewProv.Items.Refresh();
                });
            }
        } 
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void TXTOpenLastChatBTN_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!ch)
            {
                OpenChatFrame();
            }
            else
            {
                ResetColors();
                OCFrame(false);
                fro = chts = ch = false;
            }
        }
        public async void OpenChatFrame(DChat chat = null)
        {
            if(chat != null)
            {
                chatingPage.newChatWindow(chat, LogedPerson.ID);
            }
            if (fro)
            {
                OCFrame(false);
                await Task.Run(() => { Thread.Sleep(300); });
            }
            ChatingFrame.Content = chatingPage;
            ResetColors();
            TXTOpenLastChatBTN.Background = Brushes.Gainsboro;
            TXTOpenLastChatBTN.Foreground = Brushes.Teal;
            OCFrame(true);
            fro = ch = true;
            chts = false;
        }
        private void TXTAllChatsBTN_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            AllChatsBTNMethod();
        }
        async void AllChatsBTNMethod()
        {
            if (!chts)
            {
                if (fro)
                {
                    OCFrame(false);
                    await Task.Run(() => { Thread.Sleep(300); });
                }
                ChatingFrame.Content = null;
                ResetColors();
                TXTAllChatsBTN.Background = Brushes.Gainsboro;
                TXTAllChatsBTN.Foreground = Brushes.Teal;
                OCFrame(true);
                fro = chts = true;
                ch = false;
            }
            else
            {
                ResetColors();
                OCFrame(false);
                fro = chts = chts = false;
            }
        }
        public void ResetColors()
        {
            TXTOpenLastChatBTN.Background = Brushes.Teal;
            TXTAllChatsBTN.Background = Brushes.Teal;
            TXTOpenLastChatBTN.Foreground = Brushes.Gainsboro;
            TXTAllChatsBTN.Foreground = Brushes.Gainsboro;
        }
        private void TXTExitBTN_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
        private void TXTMaxMinBTN_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(WindowState != WindowState.Maximized)
            {
                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }
        private void TXTMiniBTN_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("هل تريد الخروج من النظام الأن ؟", "خروج", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.RightAlign) != MessageBoxResult.Yes){ e.Cancel = true; }
        }
        private void BTNLoginBTN_Click(object sender, RoutedEventArgs e)
        {
            Logingin();
        }
        async void Logingin()
        {
            MainLoginPanel.IsEnabled = false;
            var p = await DB.GetLogedPerson(TXTLoginUsername.Text, TXTLoginPass.Password);
            if (p != null)
            {
                //initialize loged person
                statics.LogedPerson = DPerson.GetDPerson(p);
                DataContext = statics.LogedPerson;
                var pp = await DB.GetPositionByID(statics.LogedPerson.IDPosition);
                statics.LogedPersonPosition = pp;

                //initialize components
                Initialize_Prov_Control_List();
                Initialize_Data_Manage_Pages();

                MainLoginPanel.BeginAnimation(OpacityProperty, statics.GetCDAnim(300, 1, 0));
                await Task.Run(() => { Thread.Sleep(300); });
                MainLoginPanel.Visibility = Visibility.Hidden;
                MainDockPanel.Visibility = Visibility;
                MainDockPanel.BeginAnimation(OpacityProperty, statics.GetCDAnim(300, 0, 1));
                await Task.Run(() => { Thread.Sleep(300); });
                MainDockPanel.IsEnabled = true;
            }
            else
            {
                MessageBox.Show($"فشل في تسجيل الدخول", "فشل", MessageBoxButton.OK, MessageBoxImage.Error);
                MainLoginPanel.IsEnabled = true;
            }
        }
        async Task MainDockVisibility(bool openorclose, int d = 400)
        {
            if (openorclose)
            {
                MainDockPanel.Visibility = Visibility.Visible;
                MainDockPanel.BeginAnimation(OpacityProperty, statics.GetCDAnim(d, 0, 1));
                await Task.Run(() => { Thread.Sleep(d); });
                MainDockPanel.IsEnabled = true;
            }
            else
            {
                MainDockPanel.IsEnabled = false;
                MainDockPanel.BeginAnimation(OpacityProperty, statics.GetCDAnim(d, 0, 1));
                await Task.Run(() => { Thread.Sleep(d); });
                MainDockPanel.Visibility = Visibility.Hidden;
            }
        }
        async Task MainLoginVisiblity(bool openorclose,int d = 400)
        {
            if (openorclose)
            {
                MainLoginPanel.Visibility = Visibility.Visible;
                MainLoginPanel.BeginAnimation(OpacityProperty, statics.GetCDAnim(d, 0, 1));
                await Task.Run(() => { Thread.Sleep(d); });
                MainLoginPanel.IsEnabled = true;
            }
            else
            {
                MainLoginPanel.IsEnabled = false;
                MainLoginPanel.BeginAnimation(OpacityProperty, statics.GetCDAnim(d, 1, 0));
                await Task.Run(() => { Thread.Sleep(d); });
                MainLoginPanel.Visibility = Visibility.Hidden;
            }
        }
        private void TXTLogingotoSignup_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("برجاء الاتصال بالإدارة المختصة بالمشروع\n01000000000","لا تملك حساب؟",MessageBoxButton.OK);
        }
        public static Brush Getbfroms(string hex)
        {
            try { return (SolidColorBrush)(new BrushConverter()).ConvertFromString(hex); }
            catch { return Brushes.White; }
        }
        private void MainListViewProv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(MainListViewProv.SelectedItem != null)
            {
                try
                {
                    MainFrameContainer.Content = new Views.Prov_Page((Province)MainListViewProv.SelectedItem);
                }
                catch { }

            }
        }
        void resetControlers()
        {
            if (ocprovlist) { CloseMainProvListView(); }
            MainListViewProv.SelectedIndex = -1;
            MainSettingsBTN.Background = MainDashboardBTN.Background = MainSysManageBTN.Background = MainAboutPBTN.Background = Getbfroms("#0c000000");
        }
        private void MainProvBTN_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ocprovlist) { CloseMainProvListView(); }
            else
            {
                resetControlers();
                MainProvBTN.BeginAnimation(Border.MaxHeightProperty, statics.GetCDAnim(300, 40, 300));
                ocprovlist = true;
                TXTProvOpenClose.Text = "";
                MainProvBTN.Background = Getbfroms("#33000000");
            }
        }
        private void MainSysManageBTN_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainFrameContainer.Content = new Views.SysManage.SysManageMainPage();
            resetControlers();
            MainSysManageBTN.Background = Getbfroms("#33000000");
        }
        private void MainSettingsBTN_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainFrameContainer.Content = new Views.Setting.SettingMainPage();
            resetControlers();
            MainSettingsBTN.Background = Getbfroms("#33000000");
        }
        private void MainAboutPBTN_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainFrameContainer.Content = new Views.AboutPage();
            resetControlers();
            MainAboutPBTN.Background = Getbfroms("#33000000");
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Logout();
        }

        private async void Logout()
        {
            if(MessageBox.Show("هل تريد تسجيل الخروج الان؟", "تسجيل الخروج", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.RtlReading) == MessageBoxResult.Yes)
            {
                
            }
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            AllChatsBTNMethod();
        }

        private void MainDashboardBTN_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainFrameContainer.Content = new Views.Dashboard.Dashboard_MainPage();
            resetControlers();
            MainDashboardBTN.Background = Getbfroms("#33000000");
        }
        void CloseMainProvListView()
        {
            MainProvBTN.BeginAnimation(Border.MaxHeightProperty, statics.GetCDAnim(300, 300, 40));
            ocprovlist = false;
            TXTProvOpenClose.Text = "";
            MainProvBTN.Background = Getbfroms("#0c000000");
        }
        public void OCFrame(bool openclose, int time = 400)
        {
            if (openclose) { ChatingFrame.BeginAnimation(WidthProperty, statics.GetCDAnim(time, 0, 300)); }
            else { ChatingFrame.BeginAnimation(WidthProperty, statics.GetCDAnim(time, 300, 0)); }
        }
        
    }


}
