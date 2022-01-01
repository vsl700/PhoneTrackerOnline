﻿using Microsoft.AspNetCore.Mvc;
using PhoneTracker.Models;
using PhoneTrackerOnline.Models;
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

            return View(user);
        }
    }
}