using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AZMonitoring
{
    public class DPerson : Person , INotifyPropertyChanged
    {
        private ImageSource img;
        public event PropertyChangedEventHandler PropertyChanged;
        public ImageSource Image
        {
            get
            {
                return img;
            }
            set
            {
                img = value;
                OnPropertyChanged("Image");
            }
        }
        protected void OnPropertyChanged(string name)
        {
            var handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        public string SubName { get { return Name.Split(' ').First(); } }
        internal void DownloadImage()
        {
            Task.Run(async () => { Image = await statics.DounloadImage(Photo); });
        }
        internal static DPerson GetDPerson(Person p)
        {
            DPerson d = (p as DPerson);
            if(d != null)
            {
                d.DownloadImage();
            }
            else
            {
                d = new DPerson
                {
                    Chats = p.Chats,
                    Description = p.Description,
                    DOB = p.DOB,
                    ID = p.ID,
                    IDPosition = p.IDPosition,
                    Name = p.Name,
                    Password = p.Password,
                    Photo = p.Photo,
                    SSN = p.SSN
                };
                d.DownloadImage();
            }
            return d;
        }
    }
}