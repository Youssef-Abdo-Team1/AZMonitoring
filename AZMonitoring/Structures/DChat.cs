using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AZMonitoring
{
    public class DChat : Chat, INotifyPropertyChanged
    {
        private ImageSource img;
        DAL.DAL DB = new DAL.DAL();
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
        public string Name { get; set; }
        public string Photo { get; set; }
        public List<DMessage> Messages { get; set; }
        public string LastMessage { get { return Messages.LastOrDefault().Text; } set { } }
        public DChat()
        {
            Initialize();
        }
        public void SetImage()
        {
            Task.Run(async() => { Image = await statics.DounloadImage(Photo); });
        }
        public async void Initialize()
        {
            Messages = new List<DMessage>();
            if(IDPerson1 == statics.LogedPerson.ID)
            {
                Photo = await DB.GetPersonPhoto(IDPerson2);
                Name = await DB.GetPersonName(IDPerson2);
            }
            else
            {
                Photo = await DB.GetPersonPhoto(IDPerson1);
                Name = await DB.GetPersonName(IDPerson1);
            }
            SetImage();
            (await DB.GetMessages(MessagesID)).ForEach(item => {
                Messages.Add(DMessage.GetDMessage(item));
            });
        }


        public static DChat GetDChat(Chat chat)
        {
            var d = chat as DChat;
            if(d != null)
            {

            }
            else
            {
                d = new DChat();
                d.ID = chat.ID;
                d.IDPerson1 = chat.IDPerson1;
                d.IDPerson2 = chat.IDPerson2;
                d.MessagesID = chat.MessagesID;
            }
            return d;
        }
    }
}