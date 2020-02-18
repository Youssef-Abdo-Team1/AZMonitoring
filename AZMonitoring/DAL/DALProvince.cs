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
        internal async Task<bool> AddProvince(Province mProvince,Person PHCAID,Person PLAGID,Person PCAGID,Person PWMID)
        {
            try
            {
                HashSet<String> provinceNames = (await client.GetAsync(pathprovincenames)).ResultAs<HashSet<String>>();
                if(provinceNames != null && provinceNames.Count > 0)
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
                mProvince.HCAdministrationID = new StaticProvinceInfo();
                mProvince.HCAdministrationID.PositionID = await AddPosition(new Position { Name = "مدير الادارة المركزية" , PersonID = PHCAID.ID , Level = 1, IDProvince = mProvince.Name, });
                await UpdatePersonPosition(PHCAID.ID, mProvince.HCAdministrationID.PositionID);
                mProvince.HCAdministrationID.Name = PHCAID.Name;
                mProvince.HCAdministrationID.Photo = PHCAID.Photo;

                mProvince.CulturalAgentDGID = new StaticProvinceInfo();
                mProvince.CulturalAgentDGID.PositionID = await AddPosition(new Position { Name = "الكويل الثقافي (مدير عام)", PersonID = PCAGID.ID, Level = 2, IDProvince = mProvince.Name, });
                await UpdatePersonPosition(PCAGID.ID, mProvince.CulturalAgentDGID.PositionID);
                mProvince.CulturalAgentDGID.Name = PCAGID.Name;
                mProvince.CulturalAgentDGID.Photo = PCAGID.Photo;

                mProvince.LegalAgentDGID = new StaticProvinceInfo();
                mProvince.LegalAgentDGID.PositionID = await AddPosition(new Position { Name = "الكويل الشرعي (مدير عام)", PersonID = PLAGID.ID, Level = 2, IDProvince = mProvince.Name, });
                await UpdatePersonPosition(PLAGID.ID, mProvince.LegalAgentDGID.PositionID);
                mProvince.LegalAgentDGID.Name = PLAGID.Name;
                mProvince.LegalAgentDGID.Photo = PLAGID.Photo;

                mProvince.SWelfareDID = new StaticProvinceInfo();
                mProvince.SWelfareDID.PositionID = await AddPosition(new Position { Name = "مدير رعاية الطلاب", PersonID = PWMID.ID, Level = 2, IDProvince = mProvince.Name, });
                await UpdatePersonPosition(PWMID.ID, mProvince.SWelfareDID.PositionID);
                mProvince.SWelfareDID.Name = PWMID.Name;
                mProvince.SWelfareDID.Photo = PWMID.Photo;

                await client.SetAsync(pathprovince + mProvince.Name, mProvince);
                return true;
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return false; }
        }
        internal async Task<bool> UpdateProvince(Province mProvince, Person PHCAID, Person PLAGID, Person PCAGID, Person PWMID)
        {
            try
            {
                //hca
                mProvince.HCAdministrationID.Name = PHCAID.Name;
                mProvince.HCAdministrationID.Photo = PHCAID.Photo;
                await UpdatePositionPerson(mProvince.HCAdministrationID.PositionID, PHCAID.ID);
                await UpdatePersonPosition(PHCAID.ID, mProvince.HCAdministrationID.PositionID);

                //cag
                mProvince.CulturalAgentDGID.Name = PCAGID.Name;
                mProvince.CulturalAgentDGID.Photo = PCAGID.Photo;
                await UpdatePositionPerson(mProvince.CulturalAgentDGID.PositionID, PCAGID.ID);
                await UpdatePersonPosition(PCAGID.ID, mProvince.CulturalAgentDGID.PositionID);

                //lag
                mProvince.LegalAgentDGID.Name = PLAGID.Name;
                mProvince.LegalAgentDGID.Photo = PLAGID.Photo;
                await UpdatePositionPerson(mProvince.LegalAgentDGID.PositionID, PLAGID.ID);
                await UpdatePersonPosition(PLAGID.ID, mProvince.LegalAgentDGID.PositionID);

                //swm
                mProvince.SWelfareDID.Name = PWMID.Name;
                mProvince.SWelfareDID.Photo = PWMID.Photo;
                await UpdatePositionPerson(mProvince.SWelfareDID.PositionID, PWMID.ID);
                await UpdatePersonPosition(PWMID.ID, mProvince.SWelfareDID.PositionID);


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
                if(names.Count > 0) { await client.UpdateAsync(pathprovincenames, names); }
                else { await client.DeleteAsync(pathprovincenames); }
                await client.DeleteAsync(pathprovince + ID);
                return true;
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return false; }
        }

        //listener
        internal async void SetProvinceListener()
        {
            await client.OnAsync(pathprovince, (obj, snap, cont) => { Main.Initialize_Prov_Control_List(); }, (obj, snap, cont) => { Main.Initialize_Prov_Control_List(); }, (obj, snap, cont) => { Main.Initialize_Prov_Control_List(); });
        }
    }
}
