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
                await UpdatePosition(position);
                return position.ID;
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return ""; }
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
        internal async Task<Position> GetPositionByID(string ID)
        {
            try
            {
                return (await client.GetAsync(pathposition + ID)).ResultAs<Position>();
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return null; }
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
                return (await client.GetAsync(pathposition + ID + "/Person")).ResultAs<string>();
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
