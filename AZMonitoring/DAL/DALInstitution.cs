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
        internal async Task<string> AddInstitution(Institution institution)
        {
            try
            {
                institution.ID = (await client.PushAsync(pathinstitution, institution)).Result.name;
                UpdateID(pathinstitution + institution.ID, institution.ID);
                return institution.ID;
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return ""; }
        }
        internal async Task<bool> UpdateInstitution(Institution institution)
        {
            try
            {
                await client.UpdateAsync(pathinstitution + institution.ID, institution);
                return true;
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return false; }
        }
        internal async Task<Institution> GetInstitutionbyID(string ID)
        {
            try
            {
                return (await client.GetAsync(pathinstitution + ID)).ResultAs<Institution>();
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return null; }
        }
        internal async Task<bool> DeleteInstitutionbyID(string ID)
        {
            try
            {
                await client.DeleteAsync(pathinstitution + ID);
                return true;
            }
            catch (Exception ex) { MessageBox.Show($"حدث خطأ \nكود الخطأ\n{ex.Message}", "حطأ", MessageBoxButton.OK, MessageBoxImage.Error); return false; }
        }
        internal async Task<bool> AddTeacherID(string InstitutionID ,string TeacherID)
        {
            //add teacher to list of teachers in specific Institution
            try
            {
                var ls = (await client.GetAsync(pathinstitution + InstitutionID + "/TeachersID")).ResultAs<List<string>>();
                if (ls == null || ls.Count == 0) { ls = new List<string>(); }
                ls.Add(TeacherID);
                var x = await client.SetAsync(pathinstitution + InstitutionID + "/TeachersID", ls);
                return true;
            }
            catch { return false; }
        }
        internal async Task<bool> SetAdministrationID(string InstitutionID,string AdministrationID)
        {
            //add Administration in specific Institution
            try
            {
                await client.SetAsync(pathinstitution + InstitutionID + "/AdministrationID", AdministrationID);
                return true;
            }
            catch { return false; }
        }
        internal async Task<bool> AddSheikhID(string InstitutionID, string SheikhID)
        {
            //add Sheikh in specific Institution
            try
            {
                await client.SetAsync(pathinstitution + InstitutionID + "/SheikhID", SheikhID);
                return true;
            }
            catch { return false; }
        }
    }
}
