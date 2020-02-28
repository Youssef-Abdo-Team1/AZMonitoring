using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace AZMonitoring.DAL
{
    partial class DAL
    {
        internal async Task<string> AddAdministration(Administration administration)
        {
            try
            {
                administration.ID = (await client.PushAsync(pathadministration, administration)).Result.name;
                UpdateID(pathadministration + administration.ID, administration.ID);
                await AddAdministrationsName(administration.IDProvince, administration.ID);
                return administration.ID;
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ في اضافة وظيفة\nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return ""; }
        }
        internal async Task<bool> DeleteAdministrationbyID(string ID)
        {
            try
            {
                await client.DeleteAsync(pathadministration + ID);
                return true;
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return false; }
        }
        internal async Task<bool> UpdateAdministration(Administration administration)
        {
            try
            {
                await client.UpdateAsync(pathadministration + administration.ID, administration);
                return true;
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return false; }
        }
        internal async Task<Administration> GetAdministrationByID(string AdministrationID)
        {
            try
            {
                var snap = await client.GetAsync(pathadministration + AdministrationID);
                return snap.ResultAs<Administration>();
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return null; }
        }
        internal async Task<List<Administration>> GetAdministrationsAsync(List<string> IDS)
        {
            try
            {
                if (IDS == null || IDS.Count == 0) { return null; }
                var ls = new List<Administration>();
                foreach (var item in IDS)
                {
                    ls.Add(await GetAdministrationByID(item));
                }
                return ls;
            }
            catch { return null; }
        }
    }
}
