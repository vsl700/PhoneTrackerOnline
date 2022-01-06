using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PhoneTracker.Models;
using PhoneTrackerOnline.Hubs;
using PhoneTrackerOnline.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
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


        // GET: api/target
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/target/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/target
        [HttpPost]
        public async Task<int> Post(int targetCode, [FromBody] IEnumerable<double> value)
        {
            var userID = _db.TargetPhones.Where(phone => phone.Code == targetCode).First().UserID;
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

        // PUT api/target/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/target/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
