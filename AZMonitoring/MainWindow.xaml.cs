using AZMonitoring.Structures.Pages;
using Newtonsoft.Json;
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
        internal static Views.ChatingPage chatingPage;
        private Views.All_Chats_Page achsp;
        private DAL.DAL DB;
        private bool _ShowingDialog;
        private bool _AllowClose;
        public MainWindow()
        {
            InitializeComponent();
            DB = new DAL.DAL();
            DB.CreateConnection(this);
            statics.staticframe = MainFrameContainer;
            achsp = new Views.All_Chats_Page(this);
            statics.myDH = GODH;
            //DB.test_addChats();
            //DB.Test_addpersons();
            //DB.Test_add_positions();
            //DB.Test_addProvinces();
            //DB.test_addGinstruct();
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
        internal async void Initialize_Chat(string snap)
        {
            if (statics.LogedPerson != null && statics.DChats != null)
            {
                await Dispatcher.InvokeAsync(async () => {
                    var x = await DB.GetChat(snap);
                    var y = await DChat.GetDChat(x);
                    statics.DChats.Add(y);
                    achsp.Refresh();
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
                chatingPage.newChatWindow(chat);
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
                ChatingFrame.Content = achsp;
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
        private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //if (MessageBox.Show("هل تريد الخروج من النظام الأن ؟", "خروج", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.RightAlign) != MessageBoxResult.Yes){ e.Cancel = true; }
            if (_AllowClose) return;

            //NB: Because we are making an async call we need to cancel the closing event
            e.Cancel = true;

            //we are already showing the dialog, ignore
            if (_ShowingDialog) return;

            TextBlock txt1 = new TextBlock();
            txt1.HorizontalAlignment = HorizontalAlignment.Center;
            txt1.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF53B3B"));
            txt1.Margin = new Thickness(4);
            txt1.TextWrapping = TextWrapping.WrapWithOverflow;
            txt1.FontSize = 18;
            txt1.Text = "هل تريد الخروج ؟";

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
            object result = await OpenDialogHost(stk);
            if (result is bool boolResult && boolResult)
            {
                _AllowClose = true;
                Close();
            }
        }
        private void BTNLoginBTN_Click(object sender, RoutedEventArgs e)
        {
            Logingin();
        }
        async void Logingin()
        {
            LoginBorder.IsEnabled = false;
            var p = await DB.GetLogedPerson(TXTLoginUsername.Text, TXTLoginPass.Password);
            if (p != null)
            {
                //initialize loged person
                statics.LogedPerson = DPerson.GetDPerson(p);
                DataContext = statics.LogedPerson;
                var pp = await DB.GetPositionByID(statics.LogedPerson.IDPosition);
                statics.LogedPersonPosition = pp;
                statics.DChats = new List<DChat>();
                DB.SetChatsListener(statics.LogedPerson.ID);

                //initialize components
                Initialize_Prov_Control_List();
                Initialize_Data_Manage_Pages();

                chatingPage = new Views.ChatingPage(this);

                MainLoginPanel.IsEnabled = false;
                MainLoginPanel.BeginAnimation(OpacityProperty, statics.GetCDAnim(300, 1, 0));
                await Task.Run(() => { Thread.Sleep(300); });
                MainLoginPanel.Visibility = Visibility.Hidden;
                MainDockPanel.Visibility = Visibility;
                MainDockPanel.BeginAnimation(OpacityProperty, statics.GetCDAnim(300, 0, 1));
                await Task.Run(() => { Thread.Sleep(300); });
                MainDockPanel.IsEnabled = true;
                TXTLoginUsername.Text = TXTLoginPass.Password = "";
                MainFrameContainer.Content = new Views.Dashboard.Dashboard_MainPage();
                GC.Collect();
            }
            else
            {
                MessageBox.Show($"فشل في تسجيل الدخول", "فشل", MessageBoxButton.OK, MessageBoxImage.Error);
                MainLoginPanel.IsEnabled = true;
                LoginBorder.IsEnabled = true;
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
                    statics.currentprov= (Province)MainListViewProv.SelectedItem;
                    MainFrameContainer.Content = new Views.Prov_Page(statics.currentprov);
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
                statics.LogedPerson.Image = null;
                statics.LogedPerson = null;
                statics.LogedPersonPosition = null;
                statics.Provinces = null;
                statics.Data_Mang_Pages = null;
                statics.DChats = null;
                MainDockPanel.IsEnabled = false;
                MainDockPanel.BeginAnimation(OpacityProperty, statics.GetCDAnim(300, 1, 0));
                await Task.Run(() => { Thread.Sleep(300); });
                MainDockPanel.Visibility = Visibility.Hidden;

                MainLoginPanel.Visibility = Visibility.Visible;
                MainLoginPanel.BeginAnimation(OpacityProperty, statics.GetCDAnim(300, 0, 1));
                MainLoginPanel.IsEnabled = true;
                LoginBorder.IsEnabled = true;
                MainFrameContainer.Content = null;
                resetControlers();
                ResetColors();
                OCFrame(false);
                fro = ch = chts = ocprovlist = false;
                GC.Collect();
            }
        }
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            AllChatsBTNMethod();
        }
        private void Window_StateChanged(object sender, EventArgs e)
        {
            if(WindowState == WindowState.Maximized)
            {
                bor.BorderThickness = new Thickness(8);
            }
            else { bor.BorderThickness = new Thickness(0); }
        }
        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {

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
        internal async Task<object> GODH(object cont)
        {
            object v = null;
            await Dispatcher.Invoke(async () => { v = await OpenDialogHost(cont); });
            return v;
        }
        internal async Task<object> OpenDialogHost(object content)
        {
            if (_ShowingDialog) return null;
            _ShowingDialog = true;
            object result = await MaterialDesignThemes.Wpf.DialogHost.Show(content);
            _ShowingDialog = false;
            return result;
        }
        
    }


}
