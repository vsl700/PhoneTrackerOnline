using PhoneTracker.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneTracker.Services
{
    public interface IAppointmentService
    {
        public List<TargetVM> GetPhoneTrackerList();

        public List<CallerVM> GetPatientList();
    }
}
