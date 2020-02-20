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
        internal async Task<string> AddAdmin(Admin newadmin)
        {
            try
            {
                newadmin.ID = (await client.PushAsync(pathadmin, newadmin)).Result.name;
                UpdateID(pathadmin + newadmin.ID, newadmin.ID);
                return newadmin.ID;
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return ""; }
        }
        internal async Task<bool> Updateadmin(Admin admin)
        {
            try
            {
                await client.UpdateAsync(pathadmin + admin.ID, admin);
                return true;
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return false; }
        }
        internal async Task<Admin> GetAdminbyID(string ID)
        {
            try
            {
                return (await client.GetAsync(pathprovince + ID)).ResultAs<Admin>();
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return null; }
        }
        internal async Task<string> GetAdminPositionIDbyID(string ID)
        {
            try
            {
                return (await client.GetAsync(pathprovince + ID)).ResultAs<Admin>().PositionID;
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return ""; }
        }
        internal async Task<bool> DeleteAdminbyID(string ID)
        {
            try
            {
                var p = await GetAdminbyID(ID);
                await DeletePositionbyID(p.PositionID);
                await client.DeleteAsync(pathadmin + ID);
                return true;
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return false; }
        }
        internal async Task<Position> GetAdminPositionbyID(string ID)
        {
            try
            {
                return await GetPositionByID(await GetAdminPositionIDbyID(ID));
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return null; }
        }
    }
}
