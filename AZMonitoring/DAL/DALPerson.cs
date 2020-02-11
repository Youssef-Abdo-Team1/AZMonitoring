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

        //public async Task<bool> AddPerson(Person newPerson)
        //{
        //    try
        //    {
        //        await client.SetAsync($"AZMonitoring/Person/{newPerson.ID}", newPerson);
        //        await client.SetAsync($"AZMonitoring/Person/{newPerson.ID}/Chats", "");
        //        return true;
        //    }
        //    catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return false; }
        //}
        //public async Task<bool> UpdatePersonName(Person person)
        //{
        //    try
        //    {
        //        await client.UpdateAsync($"AZMonitoring\\Person\\{person.ID}", person);
        //        return true;
        //    }
        //    catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return false; }
        //}
        //public async Task<string> GetPositionID(string PersonID)
        //{
        //    try
        //    {
        //        var snap = await client.GetAsync($"AZMonitoring\\Person\\{PersonID}\\Position");
        //        return snap.ResultAs<string>();
        //    }
        //    catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return null; }
        //}

    }
}
