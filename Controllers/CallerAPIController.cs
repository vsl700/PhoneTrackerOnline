using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneTracker.Models;
using PhoneTrackerOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PhoneTrackerOnline.Controllers
{
    [Route("api/caller")]
    [ApiController]
    public class CallerAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public CallerAPIController(ApplicationDbContext db, SignInManager<ApplicationUser> signInManager)
        {
            _db = db;
            _signInManager = signInManager;
        }

        // GET: api/caller/codes
        [HttpGet("codes")]
        public IEnumerable<int> GetTargetCodes()
        {
            if (!User.Identity.IsAuthenticated)
                throw new Exception("Validation failed!");

            User user = null;
            foreach(User tempUser in _db.CallerUsers)
            {
                if(tempUser.Username == User.Identity.Name)
                {
                    user = tempUser;
                    break;
                }
            }

            LinkedList<int> codes = new LinkedList<int>();
            foreach(var phone in _db.TargetPhones)
            {
                if (phone.UserID == user.ID)
                    codes.AddLast(phone.Code);
            }

            return codes;
        }

        // POST: api/caller/contacts
        [HttpPost("contacts")]
        public void SendContactsList([FromBody] IDictionary<string, string> contacts)
        {
            if (!User.Identity.IsAuthenticated)
                throw new Exception("Validation failed!");

            User user = null;
            foreach (User tempUser in _db.CallerUsers)
            {
                if (tempUser.Username == User.Identity.Name)
                {
                    user = tempUser;
                    break;
                }
            }

            foreach(var contact in _db.Contacts)
            {
                _db.Contacts.Remove(contact);
            }

            foreach(var kvPair in contacts)
            {
                Contact tempContact = FindContact(user, kvPair.Key);
                if(tempContact != null)
                {
                    if (tempContact.Name == kvPair.Key && tempContact.PhoneNumber == kvPair.Value)
                        continue;

                    tempContact.Name = kvPair.Key;
                    tempContact.PhoneNumber = kvPair.Value;
                    _db.Update(tempContact);
                }
                else
                {
                    tempContact = new Contact { Name=kvPair.Key, PhoneNumber=kvPair.Value, UserID=user.ID };
                    _db.Contacts.Add(tempContact);
                }
            }

            _db.SaveChangesAsync();
        }

        private Contact FindContact(User user, string name)
        {
            foreach(var contact in _db.Contacts)
            {
                if(contact.UserID == user.ID && contact.Name == name)
                {
                    return contact;
                }
            }

            return null;
        }

        // POST api/caller/login
        [HttpPost("login")]
        public async Task<bool> Login([FromBody] IEnumerable<string> user)
        {
            bool loggedIn = (User != null) && User.Identity.IsAuthenticated;
            if (loggedIn && User.Identity.Name == user.First())
                return true;

            var result = await _signInManager.PasswordSignInAsync(user.First(), user.Last(), false, false);
            return result.Succeeded;
        }
    }
}
