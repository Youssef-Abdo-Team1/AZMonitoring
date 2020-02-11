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
            DB.CreateConnection();
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
        private async void TXTAllChatsBTN_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
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
            if (MessageBox.Show("هل تريد الخروج من النظام الأن!!", "خروج", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes){ e.Cancel = true; }
        }

        private void BTNLoginBTN_Click(object sender, RoutedEventArgs e)
        {
            Logingin();
        }
        async void Logingin()
        {
            if (await DB.AddPerson(new Person { Description = "", DOB = DateTime.Now.ToString(), ChatsID = new string[] { "", "" }, ID = "ysf123", IDPosition = "", Name = "Youssef Shaaban", SSN = "wewa", Password = "123", Photo = "" }))
            {

            }
            else
            {
                MessageBox.Show($"فشل في تسجيل الدخول", "فشل", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        async Task MainDockVisibility(bool openorclose, int d = 400)
        {
            if (openorclose)
            {
                MainDockPanel.Visibility = Visibility.Visible;
                MainDockPanel.BeginAnimation(OpacityProperty, GetCDAnim(d, 0, 1));
                await Task.Run(() => { Thread.Sleep(d); });
                MainDockPanel.IsEnabled = true;
            }
            else
            {
                MainDockPanel.IsEnabled = false;
                MainDockPanel.BeginAnimation(OpacityProperty, GetCDAnim(d, 0, 1));
                await Task.Run(() => { Thread.Sleep(d); });
                MainDockPanel.Visibility = Visibility.Hidden;
            }
        }
        async Task MainLoginVisiblity(bool openorclose,int d = 400)
        {
            if (openorclose)
            {
                MainLoginPanel.Visibility = Visibility.Visible;
                MainLoginPanel.BeginAnimation(OpacityProperty, GetCDAnim(d, 0, 1));
                await Task.Run(() => { Thread.Sleep(d); });
                MainLoginPanel.IsEnabled = true;
            }
            else
            {
                MainLoginPanel.IsEnabled = false;
                MainLoginPanel.BeginAnimation(OpacityProperty, GetCDAnim(d, 1, 0));
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
                var p = (Province)MainListViewProv.SelectedItem;
            }
        }
        void resetControlers()
        {
            if (ocprovlist) { CloseMainProvListView(); }
            MainSettingsBTN.Background = MainSysManageBTN.Background = MainAboutPBTN.Background = Getbfroms("#0c000000");
        }
        private void MainProvBTN_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ocprovlist) { CloseMainProvListView(); }
            else
            {
                resetControlers();
                MainProvBTN.BeginAnimation(Border.MaxHeightProperty, GetCDAnim(300, 40, 300));
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

        void CloseMainProvListView()
        {
            MainProvBTN.BeginAnimation(Border.MaxHeightProperty, GetCDAnim(300, 300, 40));
            ocprovlist = false;
            TXTProvOpenClose.Text = "";
            MainProvBTN.Background = Getbfroms("#0c000000");
        }
        public void OCFrame(bool openclose, int time = 400)
        {
            if (openclose) { ChatingFrame.BeginAnimation(WidthProperty, GetCDAnim(time, 0, 300)); }
            else { ChatingFrame.BeginAnimation(WidthProperty, GetCDAnim(time, 300, 0)); }
        }

        public static DoubleAnimationUsingKeyFrames GetCDAnim(int Time,int invalue,int outvalue)
        {
            var anim = new DoubleAnimationUsingKeyFrames();
            var eas1 = new EasingDoubleKeyFrame(invalue, TimeSpan.FromMilliseconds(0), new CubicEase());
            var eas2 = new EasingDoubleKeyFrame(outvalue, TimeSpan.FromMilliseconds(Time), new CubicEase());
            anim.KeyFrames.Add(eas1);
            anim.KeyFrames.Add(eas2);
            return anim;
        }
    }
}
