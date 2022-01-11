using Microsoft.AspNetCore.Mvc.Rendering;
using PhoneTrackerOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneTracker.Utility
{
    public static class Helper
    {
        public static string Admin = "Admin";
        public static string Patient = "Patient";
        public static string PhoneTracker = "PhoneTracker";

        public static List<SelectListItem> GetRolesForDropDown()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{ Value=Helper.Admin, Text=Helper.Admin },
                new SelectListItem{ Value=Helper.Patient, Text=Helper.Patient },
                new SelectListItem{ Value=Helper.PhoneTracker, Text=Helper.PhoneTracker }
            };
        }

        public static List<SelectListItem> GetTimeDropDown()
        {
            int minute = 60;
            List<SelectListItem> duration = new List<SelectListItem>();
            for(int i = 0; i <= 12; i++)
            {
                duration.Add(new SelectListItem { Value = minute.ToString(), Text = i + " Hr" });
                minute = minute + 30;
                duration.Add(new SelectListItem { Value = minute.ToString(), Text = i + " Hr 30 min" });
                minute = minute + 30;
            }

            return duration;
        }

        public static ICollection<SelectListItem> ConvertContacts(List<Contact> contacts)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach(Contact contact in contacts)
            {
                items.Add(new SelectListItem { Value=contact.ID + "", Text=contact.Name + ": " + contact.PhoneNumber });
            }

            return items;
        }
    }
}
