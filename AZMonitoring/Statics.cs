using AZMonitoring.Structures.Pages;
using Firebase.Auth;
using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace AZMonitoring
{
    public delegate void MyDelegate();
    public delegate Task<object> MyDHDelegate(object content);
    public class statics
    {
        internal static Province currentprov { get; set; }
        internal static MyDelegate MessageRefreshDelegate { get; set; }
        internal static Frame staticframe { get; set; }
        internal static List<StPages> Data_Mang_Pages { get; set; }
        internal static List<Province> Provinces { get; set; }
        internal static DChat CurrentChat { get; set; }
        internal static DPerson LogedPerson { get; set; }
        internal static Position LogedPersonPosition { get; set; }
        internal static DoubleAnimationUsingKeyFrames GetCDAnim(int Time, int invalue, int outvalue)
        {
            var anim = new DoubleAnimationUsingKeyFrames();
            var eas1 = new EasingDoubleKeyFrame(invalue, TimeSpan.FromMilliseconds(0), new CubicEase());
            var eas2 = new EasingDoubleKeyFrame(outvalue, TimeSpan.FromMilliseconds(Time), new CubicEase());
            anim.KeyFrames.Add(eas1);
            anim.KeyFrames.Add(eas2);
            return anim;
        }
        internal static List<DChat> DChats { get; set; }
        internal static async Task<ImageSource> DounloadImage(string Photo)
        {
            try
            {
                if (Photo == "") { return null; }
                using (WebClient webClient = new WebClient())
                {
                    byte[] data = await webClient.DownloadDataTaskAsync(Photo);

                    using (MemoryStream mem = new MemoryStream(data))
                    {
                        return BitmapFrame.Create(mem, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                    }
                }
            }
            catch { return null; }
        }
        internal static Brush GetBrushFromString(string br)
        {
            try
            {
                return (Brush)(new BrushConverter().ConvertFromString(br));
            }
            catch { return Brushes.White; }
        }
        internal static async Task<string> UploadImage(string PersonID, FileStream img,string project, FirebaseAuthLink a)
        {
            try
            {
                var task = new FirebaseStorage(project, new FirebaseStorageOptions()
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true
                }).Child("AZMonitorimg").Child("Person").Child($"{PersonID}.jpg").PutAsync(img);
                img.Close();
                return await task;
            }
            catch { return ""; }
        }
        internal static async Task<List<string>> CreateSession(int apikey, string apisecret)
        {
            try
            {
                var ls = new List<string>();
                var p = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = Environment.CurrentDirectory + "\\SG\\OpenTokSG.exe",
                        Arguments = apikey + " " + apisecret,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    }
                };
                p.Start();
                while (!p.StandardOutput.EndOfStream)
                {
                    ls.Add(p.StandardOutput.ReadLine());
                }
                return ls;
            }
            catch { MessageBox.Show("حدث حطأ اثناء انشاء الجلسة"); return null; }
        }
        internal static MyDHDelegate myDH { get; set; }
    }
}
