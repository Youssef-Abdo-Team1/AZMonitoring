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
        internal async Task<string> AddPosition(Position position)
        {
            try
            {
                position.ID = (await client.PushAsync(pathposition, position)).Result.name;
                UpdateID(pathposition + position.ID, position.ID);
                return position.ID;
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ في اضافة وظيفة\nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return ""; }
        }
        internal async Task<bool> UpdatePosition(Position position)
        {
            try
            {
                await client.UpdateAsync(pathposition + position.ID, position);
                return true;
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return false; }
        }
        internal async Task<bool> UpdatePositionPerson(string id,string PersonID)
        {
            try
            {
                var p = await GetPositionByID(id);
                p.PersonID = PersonID;
                await client.UpdateAsync(pathposition + p.ID, p);
                return true;
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return false; }
        }
        internal async Task<Position> GetPositionByID(string ID)
        {
            try
            {
                return (await client.GetAsync(pathposition + ID)).ResultAs<Position>();
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ في جلب الوظيفة \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return null; }
        }
        internal async Task<Position> GetPositionByPorsonID(string ID)
        {
            try
            {
                return await GetPositionByID(await GetPositionID(ID));
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return null; }
        }
        internal async Task<string> GetPositionNameByID(string ID)
        {
            try
            {
                return (await client.GetAsync(pathposition + ID + "/Name")).ResultAs<string>();
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return null; }
        }
        internal async Task<string> GetPositionPersonIDByID(string ID)
        {
            try
            {
                return (await client.GetAsync(pathposition + ID + "/PersonID")).ResultAs<string>();
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return null; }
        }
        internal async Task<bool> DeletePositionbyID(string ID)
        {
            try
            {
                await client.DeleteAsync(pathposition + ID);
                return true;
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return false; }
        }

    }
}
