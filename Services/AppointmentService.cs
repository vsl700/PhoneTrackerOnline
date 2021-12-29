using PhoneTracker.Models;
using PhoneTracker.Models.ViewModels;
using PhoneTracker.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneTracker.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _db;

        public AppointmentService(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<TargetVM> GetPhoneTrackerList()
        {
            var PhoneTrackers = (from user in _db.Users
                           join userRoles in _db.UserRoles on user.Id equals userRoles.UserId
                           join roles in _db.Roles.Where
                           (x => x.Name == Helper.PhoneTracker) on userRoles.RoleId equals roles.Id
                           select new TargetVM { Id = user.Id, Name = user.Name }).ToList();
            return PhoneTrackers;
        }

        public List<CallerVM> GetPatientList()
        {
            var patient = (from user in _db.Users
                           join userRoles in _db.UserRoles on user.Id equals userRoles.UserId
                           join roles in _db.Roles.Where
                           (x => x.Name == Helper.Patient) on userRoles.RoleId equals roles.Id
                           select new CallerVM { Id = user.Id, Name = user.Name }).ToList();
            return patient;
        }
    }
}
