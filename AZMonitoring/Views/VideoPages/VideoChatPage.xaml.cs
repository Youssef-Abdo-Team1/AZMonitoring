using OpenTok;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace AZMonitoring.Views.VideoPages
{
    /// <summary>
    /// Interaction logic for VideoChatPage.xaml
    /// </summary>
    public partial class VideoChatPage : Page
    {
        private const int API_KEY = 46645842;
        private const string API_SECRET = "0bc30787c4fafa2bccc7ee7e4b0c8703cff12707";
        private string SESSION_ID = "2_MX40NjUyMTY2Mn5-MTU4MjcwMDgwOTMyM356b0N5LzYzdEp0UGVSNXlZZzlaYmUvV1l-UH4";
        private string TOKEN = "T1==cGFydG5lcl9pZD00NjUyMTY2MiZzaWc9YmViMTM5YWFmMGY3MmJiMjRkZTlhNmM1NDZmZGMyNzg1YmVmMzM3ZDpzZXNzaW9uX2lkPTJfTVg0ME5qVXlNVFkyTW41LU1UVTRNamN3TURnd09UTXlNMzU2YjBONUx6WXpkRXAwVUdWU05YbFpaemxhWW1VdlYxbC1VSDQmY3JlYXRlX3RpbWU9MTU4MjcwMDgwOSZyb2xlPXB1Ymxpc2hlciZub25jZT0xNTgyNzAwODA5LjMyNzk5MTc3ODY2NDY=";

        VideoCapturer Capturer;
        Session Session;
        Publisher Publisher;
        bool Disconnect = false;
        Dictionary<Stream, Subscriber> SubscriberByStream = new Dictionary<Stream, Subscriber>();
        public VideoChatPage()
        {
            InitializeComponent();
            // This shows how to enumarate the available capturer devices on the system to allow the user of the app
            // to select the desired camera. If a capturer is not provided in the publisher constructor the first available 
            // camera will be used.
            
        }
        internal async Task<List<string>> Createvideochat()
        {
            var ls = await statics.CreatePublisherSession(API_KEY, API_SECRET);
            if (ls[0].Contains("Error:")) { return null; }
            SESSION_ID = ls[0];
            TOKEN = ls[1];
            Initiate();
            return ls;
        }
        internal void EnterChat(string sessionid,string token)
        {
            SESSION_ID = sessionid;
            TOKEN = token;
            Initiate();
        }
        void Initiate()
        {
            var devices = VideoCapturer.EnumerateDevices();
            if (devices.Count > 0)
            {
                var selectedDevice = devices[0];
                Trace.WriteLine("Using camera: " + devices[0].Name);
                Capturer = selectedDevice.CreateVideoCapturer(VideoCapturer.Resolution.High);
            }
            else
            {
                MessageBox.Show("Warning: no cameras available, the publisher will be audio only.");
            }

            // We create the publisher here to show the preview when application starts
            // Please note that the PublisherVideo component is added in the xaml file
            Publisher = new Publisher(Context.Instance, renderer: PublisherVideo, capturer: Capturer);

            if (API_KEY == 0 || SESSION_ID == "" || TOKEN == "")
            {
                MessageBox.Show("Please fill out the API_KEY, SESSION_ID and TOKEN variables in the source code " +
                    "in order to connect to the session", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Session = new Session(Context.Instance, API_KEY.ToString(), SESSION_ID);

                Session.Connected += Session_Connected;
                Session.Disconnected += Session_Disconnected;
                Session.Error += Session_Error;
                Session.StreamReceived += Session_StreamReceived;
                Session.StreamDropped += Session_StreamDropped;
            }


            if (Disconnect)
            {
                MessageBox.Show("Disconnecting session");
                try
                {
                    Session.Unpublish(Publisher);
                    Session.Disconnect();
                }
                catch (OpenTokException ex)
                {
                    MessageBox.Show("OpenTokException " + ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Connecting session");
                try
                {
                    Session.Connect(TOKEN);
                }
                catch (OpenTokException ex)
                {
                    MessageBox.Show("OpenTokException " + ex.ToString());
                }
            }
            Disconnect = !Disconnect;
        }
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (var subscriber in SubscriberByStream.Values)
            {
                subscriber.Dispose();
            }
            Publisher?.Dispose();
            Capturer?.Dispose();
            Session?.Dispose();
        }

        private void Session_Connected(object sender, EventArgs e)
        {
            try
            {
                Session.Publish(Publisher);
            }
            catch (OpenTokException ex)
            {
                MessageBox.Show("OpenTokException " + ex.ToString());
            }
        }

        private void Session_Disconnected(object sender, EventArgs e)
        {
            MessageBox.Show("Session disconnected");
            SubscriberByStream.Clear();
            SubscriberGrid.Children.Clear();
        }

        private void Session_Error(object sender, Session.ErrorEventArgs e)
        {
            Trace.WriteLine("Session error:" + e.ErrorCode);
            MessageBox.Show("Session error:" + e.ErrorCode, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void UpdateGridSize(int numberOfSubscribers)
        {
            int rows = Convert.ToInt32(Math.Round(Math.Sqrt(numberOfSubscribers)));
            int cols = rows == 0 ? 0 : Convert.ToInt32(Math.Ceiling(((double)numberOfSubscribers) / rows));
            SubscriberGrid.Columns = cols;
            SubscriberGrid.Rows = rows;
        }

        private void Session_StreamReceived(object sender, Session.StreamEventArgs e)
        {
            MessageBox.Show("Session stream received");

            VideoRenderer renderer = new VideoRenderer();
            SubscriberGrid.Children.Add(renderer);
            UpdateGridSize(SubscriberGrid.Children.Count);
            Subscriber subscriber = new Subscriber(Context.Instance, e.Stream, renderer);
            SubscriberByStream.Add(e.Stream, subscriber);

            try
            {
                Session.Subscribe(subscriber);
            }
            catch (OpenTokException ex)
            {
                Trace.WriteLine("OpenTokException " + ex.ToString());
            }
        }

        private void Session_StreamDropped(object sender, Session.StreamEventArgs e)
        {
            MessageBox.Show("Session stream dropped");
            var subscriber = SubscriberByStream[e.Stream];
            if (subscriber != null)
            {
                SubscriberByStream.Remove(e.Stream);
                try
                {
                    Session.Unsubscribe(subscriber);
                }
                catch (OpenTokException ex)
                {
                    Trace.WriteLine("OpenTokException " + ex.ToString());
                }

                SubscriberGrid.Children.Remove((UIElement)subscriber.VideoRenderer);
                UpdateGridSize(SubscriberGrid.Children.Count);
            }
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            if (Disconnect)
            {
                MessageBox.Show("Disconnecting session");
                try
                {
                    Session.Unpublish(Publisher);
                    Session.Disconnect();
                }
                catch (OpenTokException ex)
                {
                    MessageBox.Show("OpenTokException " + ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Connecting session");
                try
                {
                    Session.Connect(TOKEN);
                }
                catch (OpenTokException ex)
                {
                    MessageBox.Show("OpenTokException " + ex.ToString());
                }
            }
            Disconnect = !Disconnect;
            //ConnectDisconnectButton.Content = Disconnect ? "Disconnect" : "Connect";
        }

        internal void disconnect()
        {
            try { Session.Disconnect(); } catch { }
        }
    }
}
