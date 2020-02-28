using OpenTokSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG
{
    public class Class1
    {
        public static List<string> CreateSession(int apikey,string apisecret)
        {
            try
            {
                var ls = new List<string>();
                var ot = new OpenTok(apikey, apisecret);
                ls.Add(ot.CreateSession().Id);
                ls.Add(ot.GenerateToken(ls[0]));
                return ls;
            }
            catch { MessageBox.Show("حدث حطأ اثناء انشاء الجلسة"); return null; }
        }
    }
}
