using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZMonitoring.DAL
{
    partial class DAL
    {
        IDisposable streaming;
        internal void SetStreaingListener(myLiveStreamdeleget myLive)
        {
            streaming = firebase.Child("LiveStreaming").AsObservable<LiveStream>().Subscribe(d => { myLive.Invoke(d.Object); });
        }
        internal void DisposeStreamingListener()
        {
            streaming?.Dispose();
        }
    }
}
