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
                if (trackVM.PhoneID == 0)
                {
                    int newCode;
                    Random r = new Random();
                    do
                    {
                        newCode = r.Next(100000, 1000000);
                    } while (GetTargetPhone(newCode, false) != null || GetTargetPhone(newCode, true) != null);


                    _db.TargetPhones.Add(new TargetPhone { Name=trackVM.PhoneName, UserID=user.ID, Code=newCode, OldCode=newCode, ContactID=trackVM.PhoneContactID });
                    var contact = _db.Contacts.Find(trackVM.PhoneContactID);
                    _db.Contacts.Update(contact);
                }
                else
                {
                    _db.TargetPhones.Update(new TargetPhone { ID=trackVM.PhoneID, Name = trackVM.PhoneName, ContactID=trackVM.PhoneContactID });
                }
                _db.SaveChanges();

                return RedirectToAction("Index");
            }

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
                    return true;
            }

            return false;
        }
    }
}
