using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PhoneTracker.Models;
using PhoneTracker.Utility;
using PhoneTrackerOnline.Hubs;
using PhoneTrackerOnline.Interface;
using PhoneTrackerOnline.Models;
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
            
            List<TargetPhone> tempPhones = new List<TargetPhone>();
            foreach (TargetPhone targetPhone in _db.TargetPhones)
            {
                if(targetPhone.UserID == user.ID)
                {
                    tempPhones.Add(targetPhone);
                }
            }
            user.TrackedPhones = tempPhones;

            return View(user);
        }
    }
}
