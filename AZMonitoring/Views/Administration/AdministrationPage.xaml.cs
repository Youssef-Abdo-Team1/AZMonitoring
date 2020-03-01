using System;
using System.Collections.Generic;
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

namespace AZMonitoring.Views.Administration
{
    /// <summary>
    /// Interaction logic for AdministrationPage.xaml
    /// </summary>
    public partial class AdministrationPage : Page
    {

        AZMonitoring.Administration Administration;
        List<Instruct> instructs;
        List<Institution> IPN;
        List<Institution> IPT;
        List<Institution> IPS;
        List<Institution> IPRN;
        List<Institution> IPRT;
        List<Institution> IPRS;
        List<Institution> ISN;
        List<Institution> IST;
        List<Institution> ISS;
        DAL.DAL DB;

        public AdministrationPage()
        {
            InitializeComponent();

            DB = new DAL.DAL();

            instructs = new List<Instruct>();
            LVInstructs.ItemsSource = instructs;

            IPN = new List<Institution>();
            IPT = new List<Institution>();
            IPS = new List<Institution>();
            IPRN = new List<Institution>();
            IPRT = new List<Institution>();
            IPRS = new List<Institution>();
            ISN = new List<Institution>();
            IST = new List<Institution>();
            ISS = new List<Institution>();
            LVPStagesN.ItemsSource = IPN;
            LVPStagesT.ItemsSource = IPT;
            LVPStagesS.ItemsSource = IPS;
            LVPRStagesN.ItemsSource = IPRN;
            LVPRStagesT.ItemsSource = IPRT;
            LVPRStagesS.ItemsSource = IPRS;
            LVSStagesN.ItemsSource = ISN;
            LVSStagesT.ItemsSource = IST;
            LVSStagesS.ItemsSource = ISS;
        }
        internal void Initialize_Administration(AZMonitoring.Administration administration)
        {
            ClearValues();
            Administration = administration;
            InitiateInstitutions();
            InitializeInstructs();
            TXTAdName.Text = $"{Administration.Name} التعليمية بمنظقة {Administration.IDProvince} الازهرية";
            TXTAdS.Text = $"المعاهد ب{Administration.Name} التعليمية بمنظقة {Administration.IDProvince} الازهرية";
            TXTAdDes.Text = Administration.Description;
        }
        async void InitializeInstructs()
        {
            if(Administration.InstrutsID != null)
            {
                foreach (var item in Administration.InstrutsID)
                {
                    instructs.Add(await DB.GetInstructbyID(item));
                }
            }
            LVInstructs.Items.Refresh();
        }
        void ClearValues()
        {
            instructs.Clear();
            IPN.Clear();
            IPT.Clear();
            IPS.Clear();
            IPRN.Clear();
            IPRT.Clear();
            IPRS.Clear();
            ISN.Clear();
            IST.Clear();
            ISS.Clear();
            RefreshLVs();
        }
        async void InitiateInstitutions()
        {
            Institution ins;
            if (Administration.InstitutionsID != null)
            {
                foreach (var item in Administration.InstitutionsID)
                {
                    ins = await DB.GetInstitutionbyID(item);
                    if (ins != null)
                    {
                        if (ins.Stage == Stages.PrimaryStage)
                        {
                            if (ins.Type == Type.Normal) { IPN.Add(ins); }
                            else if (ins.Type == Type.Typical) { IPT.Add(ins); }
                            else if (ins.Type == Type.Private) { IPS.Add(ins); }
                        }
                        else if (ins.Stage == Stages.PreparatoryStage)
                        {
                            if (ins.Type == Type.Normal) { IPRN.Add(ins); }
                            else if (ins.Type == Type.Typical) { IPRT.Add(ins); }
                            else if (ins.Type == Type.Private) { IPRS.Add(ins); }
                        }
                        else if (ins.Stage == Stages.SecondaryStage)
                        {
                            if (ins.Type == Type.Normal) { ISN.Add(ins); }
                            else if (ins.Type == Type.Typical) { IST.Add(ins); }
                            else if (ins.Type == Type.Private) { ISS.Add(ins); }
                        }
                    }
                }
            }
            RefreshLVs();
        }
        private void RefreshLVs()
        {
            LVInstructs.Items.Refresh();
            LVPStagesN.Items.Refresh();
            LVPStagesT.Items.Refresh();
            LVPStagesS.Items.Refresh();
            LVPRStagesN.Items.Refresh();
            LVPRStagesT.Items.Refresh();
            LVPRStagesS.Items.Refresh();
            LVSStagesN.Items.Refresh();
            LVSStagesT.Items.Refresh();
            LVSStagesS.Items.Refresh();
        }
        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }
    }
}
