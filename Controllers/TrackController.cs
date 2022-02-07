using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PhoneTracker.Models;
using PhoneTracker.Utility;
using PhoneTrackerOnline.Hubs;
using PhoneTrackerOnline.Interface;
using PhoneTrackerOnline.Models;
using PhoneTrackerOnline.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhoneTrackerOnline.Controllers
{
    public class TrackController : Controller
    {
        private ApplicationDbContext _db;

        public TrackController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            bool loggedIn = (User != null) && User.Identity.IsAuthenticated;
            if (!loggedIn)
                return RedirectToAction("Login", "Account");

            var query = _db.CallerUsers.AsQueryable();
            User user = query.Where(user => user.Username == User.Identity.Name).First();

            ConfigureUser(user);

            TrackVM trackVM = new TrackVM { Caller=user };

            ViewBag.PhoneIDError = -1;

            return View(trackVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(TrackVM trackVM)
        {
            var query = _db.CallerUsers.AsQueryable();
            User user = query.Where(user => user.Username == User.Identity.Name).First();

            if (ModelState.IsValid)
            {
                if (trackVM.PhoneID == 0) // Add
                {
                    int newCode;
                    Random r = new Random();
                    do
                    {
                        newCode = r.Next(100000, 1000000);
                    } while (GetTargetPhone(newCode, false) != null || GetTargetPhone(newCode, true) != null);


                    _db.TargetPhones.Add(new TargetPhone { Name = trackVM.PhoneName, UserID = user.ID, Code = newCode, OldCode = newCode, ContactID = trackVM.PhoneContactID });
                    _db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else // Edit
                {
                    var phone = _db.TargetPhones.Find(trackVM.PhoneID);
                    phone.Name = trackVM.PhoneName;
                    if(trackVM.PhoneContactID != -1) // -1 means no change in the Phone's Contact; 0 means no contact; any positive number means a ContactID
                        phone.ContactID = trackVM.PhoneContactID;

                    _db.TargetPhones.Update(phone);
                    _db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }

            ViewBag.PhoneIDError = trackVM.PhoneID;

            ConfigureUser(user);
            trackVM.Caller = user;
            return View(trackVM);
        }

        private TargetPhone GetTargetPhone(int code, bool oldCode)
        {
            var phonesList = _db.TargetPhones.Where(phone => oldCode && phone.OldCode == code || !oldCode && phone.Code == code);
            if (phonesList.Count() > 0)
                return phonesList.First();

            return null;
        }

        private void ConfigureUser(User user)
        {
            List<TargetPhone> tempPhones = new List<TargetPhone>();
            foreach (TargetPhone targetPhone in _db.TargetPhones)
            {
                if (targetPhone.UserID == user.ID)
                {
                    tempPhones.Add(targetPhone);
                }
            }
            user.TrackedPhones = tempPhones;

            List<Contact> tempUserContacts = new List<Contact>();
            ViewBag.ContactStrings = new Dictionary<int, string>();
            foreach (Contact contact in _db.Contacts)
            {
                if (contact.UserID == user.ID && !IsContactTaken(contact))
                {
                    tempUserContacts.Add(contact);
                }
            }
            ViewBag.Contacts = Helper.ConvertContacts(tempUserContacts);
        }

        private bool IsContactTaken(Contact contact)
        {
            foreach(var phone in _db.TargetPhones)
            {
                if (phone.ContactID == contact.ID)
                {
                    ViewBag.ContactStrings[phone.ID] = Helper.GenerateContactListTag(contact);
                    return true;
                }
            }

            return false;
        }
    }
}
