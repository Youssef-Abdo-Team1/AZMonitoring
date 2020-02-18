using AZMonitoring.Structures.Pages;
using Firebase.Auth;
using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace AZMonitoring
{
    public class statics
    {
        internal static Frame staticframe { get; set; }
        internal static List<StPages> Data_Mang_Pages { get; set; }
        internal static List<Province> Provinces { get; set; }
        internal static DoubleAnimationUsingKeyFrames GetCDAnim(int Time, int invalue, int outvalue)
        {
            var anim = new DoubleAnimationUsingKeyFrames();
            var eas1 = new EasingDoubleKeyFrame(invalue, TimeSpan.FromMilliseconds(0), new CubicEase());
            var eas2 = new EasingDoubleKeyFrame(outvalue, TimeSpan.FromMilliseconds(Time), new CubicEase());
            anim.KeyFrames.Add(eas1);
            anim.KeyFrames.Add(eas2);
            return anim;
        }
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
    }
}
