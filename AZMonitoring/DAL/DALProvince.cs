using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AZMonitoring.DAL
{
    partial class DAL
    {
        internal async Task<bool> AddProvince(Province mProvince, Person PHCAID, Person PLAGID, Person PCAGID, Person PWMID)
        {
            try
            {
                await Task.Run(async () =>
                 {
                     HashSet<String> provinceNames = (await client.GetAsync(pathprovincenames)).ResultAs<HashSet<String>>();
                     if (provinceNames != null && provinceNames.Count > 0)
                     {
                         provinceNames.Add(mProvince.Name);
                         await client.SetAsync(pathprovincenames, provinceNames);
                     }
                     else
                     {
                         provinceNames = new HashSet<string>();
                         provinceNames.Add(mProvince.Name);
                         await client.SetAsync(pathprovincenames, provinceNames);
                     }
                 });
                mProvince.HCAdministrationID = new StaticInfo();
                mProvince.CulturalAgentDGID = new StaticInfo();
                mProvince.LegalAgentDGID = new StaticInfo();
                mProvince.SWelfareDID = new StaticInfo();

                mProvince.HCAdministrationID.Name = PHCAID.Name;
                mProvince.HCAdministrationID.Photo = PHCAID.Photo;


                mProvince.CulturalAgentDGID.Name = PCAGID.Name;
                mProvince.CulturalAgentDGID.Photo = PCAGID.Photo;


                mProvince.LegalAgentDGID.Name = PLAGID.Name;
                mProvince.LegalAgentDGID.Photo = PLAGID.Photo;


                mProvince.SWelfareDID.Name = PWMID.Name;
                mProvince.SWelfareDID.Photo = PWMID.Photo;

                mProvince.HCAdministrationID.PositionID = await AddPosition(new Position { Name = "مدير الادارة المركزية", PersonID = PHCAID.ID, Level = 1, IDProvince = mProvince.Name, });
                mProvince.CulturalAgentDGID.PositionID = await AddPosition(new Position { Name = "الكويل الثقافي (مدير عام)", PersonID = PCAGID.ID, Level = 2, IDProvince = mProvince.Name, });
                mProvince.LegalAgentDGID.PositionID = await AddPosition(new Position { Name = "الكويل الشرعي (مدير عام)", PersonID = PLAGID.ID, Level = 2, IDProvince = mProvince.Name, });
                mProvince.SWelfareDID.PositionID = await AddPosition(new Position { Name = "مدير رعاية الطلاب", PersonID = PWMID.ID, Level = 2, IDProvince = mProvince.Name, });

                _ = UpdatePersonPosition(PWMID.ID, mProvince.SWelfareDID.PositionID);
                _ = UpdatePersonPosition(PHCAID.ID, mProvince.HCAdministrationID.PositionID);
                _ = UpdatePersonPosition(PCAGID.ID, mProvince.CulturalAgentDGID.PositionID);
                _ = UpdatePersonPosition(PLAGID.ID, mProvince.LegalAgentDGID.PositionID);

                await client.SetAsync(pathprovince + mProvince.Name, mProvince);
                return true;
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ اضافة المحافظة\nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return false; }
        }
        internal async Task<bool> UpdateProvince(Province mProvince, Person PHCAID, Person PLAGID, Person PCAGID, Person PWMID)
        {
            try
            {
                //hca
                mProvince.HCAdministrationID.Name = PHCAID.Name;
                mProvince.HCAdministrationID.Photo = PHCAID.Photo;
                UpdatePositionPerson(mProvince.HCAdministrationID.PositionID, PHCAID.ID).Start();
                UpdatePersonPosition(PHCAID.ID, mProvince.HCAdministrationID.PositionID).Start();

                //cag
                mProvince.CulturalAgentDGID.Name = PCAGID.Name;
                mProvince.CulturalAgentDGID.Photo = PCAGID.Photo;
                UpdatePositionPerson(mProvince.CulturalAgentDGID.PositionID, PCAGID.ID).Start();
                UpdatePersonPosition(PCAGID.ID, mProvince.CulturalAgentDGID.PositionID).Start();

                //lag
                mProvince.LegalAgentDGID.Name = PLAGID.Name;
                mProvince.LegalAgentDGID.Photo = PLAGID.Photo;
                UpdatePositionPerson(mProvince.LegalAgentDGID.PositionID, PLAGID.ID).Start();
                UpdatePersonPosition(PLAGID.ID, mProvince.LegalAgentDGID.PositionID).Start();

                //swm
                mProvince.SWelfareDID.Name = PWMID.Name;
                mProvince.SWelfareDID.Photo = PWMID.Photo;
                UpdatePositionPerson(mProvince.SWelfareDID.PositionID, PWMID.ID).Start();
                UpdatePersonPosition(PWMID.ID, mProvince.SWelfareDID.PositionID).Start();


                await client.UpdateAsync(pathprovince + mProvince.Name, mProvince);
                return true;
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return false; }
        }
        internal async Task<List<string>> GetProvinceNames()
        {
            try
            {
                return (await client.GetAsync(pathprovincenames)).ResultAs<List<string>>();
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return null; }
        }
        internal async Task<Province> GetProvincebyName(string name)
        {
            try
            {
                return (await client.GetAsync(pathprovince + name)).ResultAs<Province>();
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return null; }
        }
        internal async Task<List<Province>> GetAllProvinces()
        {
            try
            {
                List<Province> lis = new List<Province>();
                foreach (var item in (await GetProvinceNames()))
                {
                    lis.Add((await GetProvincebyName(item)));
                }
                return lis;
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return null; }
        }
        internal async Task<bool> DeleteProvincebyID(string ID)
        {
            try
            {
                var p = await GetProvincebyName(ID);
                await DeletePositionbyID(p.CulturalAgentDGID.PositionID);
                await DeletePositionbyID(p.HCAdministrationID.PositionID);
                await DeletePositionbyID(p.LegalAgentDGID.PositionID);
                await DeletePositionbyID(p.SWelfareDID.PositionID);
                try { p.GInstructsID.ForEach(item => { }); } catch { }
                try { p.AdministrationsID.ForEach(item => { }); } catch { }
                List<string> names = await GetProvinceNames();
                names.Remove(ID);
                if (names.Count > 0) { await client.UpdateAsync(pathprovincenames, names); }
                else { await client.DeleteAsync(pathprovincenames); }
                await client.DeleteAsync(pathprovince + ID);
                return true;
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return false; }
        }
        //listener
        internal async void SetProvinceListener()
        {
            try { await client.OnAsync(pathprovince, (obj, snap, cont) => { Main.Initialize_Prov_Control_List(); }, (obj, snap, cont) => { Main.Initialize_Prov_Control_List(); }, (obj, snap, cont) => { Main.Initialize_Prov_Control_List(); }); }
            catch { }
        }
        internal async Task<bool> AddAdministrationsName(string name, string id)
        {
            try
            {
                var ls = (await client.GetAsync(pathprovince + name + "/AdministrationsID")).ResultAs<List<string>>();
                if (ls == null || ls.Count == 0) { ls = new List<string>(); }
                ls.Add(id);
                var x = await client.SetAsync(pathprovince + name + "/AdministrationsID/", ls);

                return true;
            }
            catch { return false; }
        }
        internal async void AddGInstructToProvince(string provid, string id)
        {
            try
            {
                List<string> ls = (await client.GetAsync(pathprovince + provid + "/GInstructsID")).ResultAs<List<string>>();
                if (ls == null) { ls = new List<string>(); }
                ls.Add(id);
                await client.SetAsync(pathprovince + provid + "/GInstructsID", ls);
            }
            catch { }
        }
    }
}
