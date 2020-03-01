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
        internal async Task<string> AddInstruct(Instruct instruct)
        {
            try
            {
                instruct.ID = (await client.PushAsync(pathinstruct, instruct)).Result.name;
                UpdateID(pathginstruct + instruct.ID, instruct.ID);
                AddInstructToGInstruct(instruct.IDGInstruct, instruct.ID);
                AddInstructToAdministration(instruct.IDGInstruct, instruct.ID);
                return instruct.ID;
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return ""; }
        }
        internal async Task<bool> UpdateInstruct(Instruct instruct)
        {
            try
            {
                await client.UpdateAsync(pathinstruct + instruct.ID, instruct);
                return true;
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return false; }
        }
        internal async Task<Instruct> GetInstructbyID(string ID)
        {
            try
            {
                return (await client.GetAsync(pathinstruct + ID)).ResultAs<Instruct>();
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return null; }
        }
        internal async Task<bool> DeleteInstructbyID(string ID)
        {
            try
            {
                await client.DeleteAsync(pathinstruct + ID);
                return true;
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return false; }
        }
        internal async Task<bool> Add_Adminstration_Instructors_ID(string InstructID, string AdminstrationInstructorsID)
        {
            //add teacher to list of teachers in specific Institution
            try
            {
                var ls = (await client.GetAsync(pathinstruct + InstructID + "/AdminstrationInstructorsID")).ResultAs<List<string>>();
                if (ls == null || ls.Count == 0) { ls = new List<string>(); }
                ls.Add(AdminstrationInstructorsID);
                var x = await client.SetAsync(pathinstruct + InstructID + "/AdminstrationInstructorsID", ls);
                return true;
            }
            catch { return false; }
        }
        internal async Task<bool> SetIDGInstruct(string InstructID, string IDGInstruct)
        {
            //add Administration in specific Institution
            try
            {
                await client.SetAsync(pathinstitution + InstructID + "/IDGInstruct", IDGInstruct);
                return true;
            }
            catch { return false; }
        }
        internal async Task<bool> AddIDAdministration(string InstructID, string IDAdministration)
        {
            //add Sheikh in specific Institution
            try
            {
                await client.SetAsync(pathinstitution + InstructID + "/IDAdministration", IDAdministration);
                return true;
            }
            catch { return false; }
        }
        internal async Task<List<Instruct>> GetInstructs(List<string> IDS)
        {
            try
            {
                if (IDS == null || IDS.Count == 0) { return null; }
                var ls = new List<Instruct>();
                foreach (var item in IDS)
                {
                    ls.Add(await GetInstructbyID(item));
                }
                return ls;
            }
            catch { return null; }
        }
    }
}
