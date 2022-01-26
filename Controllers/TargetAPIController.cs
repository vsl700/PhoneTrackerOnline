using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PhoneTracker.Models;
using PhoneTrackerOnline.Hubs;
using PhoneTrackerOnline.Interface;
using PhoneTrackerOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PhoneTrackerOnline.Controllers
{
    [Route("api/target")]
    [ApiController]
    public class TargetAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IHubContext<NotificationUserHub> _notificationUserHubContext;
        private readonly IUserConnectionManager _userConnectionManager;

        public TargetAPIController(ApplicationDbContext db, IHubContext<NotificationUserHub> notificationUserHubContext, 
            IUserConnectionManager userConnectionManager)
        {
            _db = db;
            _notificationUserHubContext = notificationUserHubContext;
            _userConnectionManager = userConnectionManager;
        }

        // POST api/target
        [HttpPost]
        public async Task<int> SendCurrentLocation(int targetCode, [FromBody] IEnumerable<double> value) // Target ONLY
        {
            var targetPhone = GetTargetPhone(targetCode, false);
            if(targetPhone == null)
                throw new Exception("Validation failed!");

            var userID = targetPhone.UserID;
            var userName = _db.CallerUsers.Find(userID).Username;

            var connections = _userConnectionManager.GetUserConnections(userName);
            if (connections != null && connections.Count > 0)
            {
                foreach (var connectionId in connections)
                {
                    await _notificationUserHubContext.Clients.Client(connectionId).SendAsync("sendToUser", value.First(), value.Last());
                }

                return connections.Count;
            }

            return 0;
        }

        // POST api/target/locslist
        [HttpPost("locslist")]
        public void SendLocationsList(int targetCode, [FromBody] IEnumerable<string> locations)
        {
            var targetPhone = GetTargetPhone(targetCode, false);
            if (targetPhone == null || !ValidateAccess(targetPhone))
                throw new Exception("Validation failed!");

            var elementsArray = locations.Select(elem => elem.Split(';'));

            for (int i = 0; i < locations.Count(); i++)
            {
                string capTime = elementsArray.ElementAt(i)[3];
                bool flag = false;
                foreach(Location location in _db.Locations)
                {
                    if (location.TargetPhoneID != targetPhone.ID)
                        continue;

                    if(location.TimeTaken == capTime) // We only add new locations!
                    {
                        flag = true;
                        break;
                    }
                }

                if (flag)
                    continue;


                Location newLoc = new Location { Latitude=double.Parse(elementsArray.ElementAt(i)[0]), Longitude=double.Parse(elementsArray.ElementAt(i)[1]), MarkerColor=int.Parse(elementsArray.ElementAt(i)[2]), TimeTaken=capTime, TargetPhoneID=targetPhone.ID };
                _db.Locations.Add(newLoc);
            }

            _db.SaveChanges();
        }

        // GET api/target/locslist
        [HttpGet("locslist")]
        public IEnumerable<string> GetLocationsList(int targetCode) // Caller ONLY
        {
            var targetPhone = GetTargetPhone(targetCode, false);
            if (targetPhone == null || !ValidateAccess(targetPhone))
                throw new Exception("Validation failed!");

            LinkedList<string> locations = new LinkedList<string>();
            StringBuilder sb = new StringBuilder();
            foreach(Location location in _db.Locations)
            {
                if (location.TargetPhoneID != targetPhone.ID)
                    continue;

                sb.Append(location.Latitude); sb.Append(';');
                sb.Append(location.Longitude); sb.Append(';');
                sb.Append(location.MarkerColor); sb.Append(';');
                sb.Append(location.TimeTaken);

                locations.AddLast(sb.ToString());

                sb.Clear();
            }

            return locations;
        }

        // GET api/target/contact
        [HttpGet("contact")]
        public string GetTargetNumber(int targetCode) // Caller ONLY
        {
            var targetPhone = GetTargetPhone(targetCode, false);
            if (targetPhone == null || !ValidateAccess(targetPhone))
                throw new Exception("Validation failed!");

            var contact = _db.Contacts.Find(targetPhone.ContactID);
            if(contact != null)
                return contact.PhoneNumber;

            return "-1";
        }

        // POST api/target/login
        [HttpPost("login")]
        public bool Login(int targetCode) // Target ONLY
        {
            var targetPhone = GetTargetPhone(targetCode, true);
            if (targetPhone == null)
                return false;

            return true;
        }

        // GET api/target/code
        [HttpGet("code")]
        public int GetTargetCode(int oldCode) // Target ONLY
        {
            var targetPhone = GetTargetPhone(oldCode, true);
            if (targetPhone == null)
                throw new Exception("Validation failed!");

            targetPhone.OldCode = targetPhone.Code;
            _db.Update(targetPhone);
            _db.SaveChanges();
            

            return targetPhone.Code;
        }

        // GET api/target/oldcode
        [HttpGet("oldcode")]
        public int GetTargetOldCode(int targetCode) // Caller ONLY
        {
            var targetPhone = GetTargetPhone(targetCode, false);
            if (targetPhone == null || !ValidateAccess(targetPhone))
                throw new Exception("Validation failed!");

            return targetPhone.OldCode;
        }

        // GET api/target/changecodereq
        [HttpGet("changecodereq")]
        public int ChangeCodeRequest(int targetCode)
        {
            var targetPhone = GetTargetPhone(targetCode, true);
            if (targetPhone == null || !ValidateAccess(targetPhone))
                throw new Exception("Validation failed!");

            int newCode;
            Random r = new Random();
            do
            {
                newCode = r.Next(100000, 1000000);
            } while (GetTargetPhone(newCode, false) != null || GetTargetPhone(newCode, true) != null);

            targetPhone.Code = newCode;
            if (!ValidateAccess(targetPhone)) // If the target has logged in
                targetPhone.OldCode = newCode;

            _db.Update(targetPhone);
            _db.SaveChanges();

            return newCode;
        }

        private TargetPhone GetTargetPhone(int code, bool oldCode)
        {
            var phonesList = _db.TargetPhones.Where(phone => oldCode && phone.OldCode == code || !oldCode && phone.Code == code);
            if (phonesList.Count() > 0)
                return phonesList.First();

            return null;
        }

        private bool ValidateAccess(TargetPhone targetPhone)
        {
            return User.Identity.IsAuthenticated && User.Identity.Name == _db.CallerUsers.Find(targetPhone.UserID).Username;
        }
    }
}
