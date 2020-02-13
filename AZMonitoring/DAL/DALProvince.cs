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
        public async Task<bool> AddProvince(Province mProvince)
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
                await client.SetAsync(pathprovince + mProvince.Name, mProvince);
                return true;
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return false; }
        }
        public async Task<bool> UpdateProvince(Province mProvince)
        {
            try
            {
                await client.UpdateAsync(pathprovince + mProvince.Name, mProvince);
                return true;
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return false; }
        }
        public async Task<List<string>> GetProvinceNames()
        {
            try
            {
                return (await client.GetAsync(pathprovincenames)).ResultAs<List<string>>();
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return null; }
        }
        public async Task<Province> GetProvincebyName(string name)
        {
            try
            {
                return (await client.GetAsync(pathprovince + name)).ResultAs<Province>();
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return null; }
        }
        public async Task<List<Province>> GetAllProvinces()
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
    }
}
