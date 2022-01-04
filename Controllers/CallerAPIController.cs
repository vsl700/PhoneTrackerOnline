using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneTracker.Models;
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
        private readonly SignInManager<ApplicationUser> _signInManager;

        public CallerAPIController(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
        }

        // GET: api/caller
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/caller/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/caller
        [HttpPost]
        public async Task<bool> Post([FromBody] IEnumerable<string> user)
        {
            /*bool loggedIn = (User != null) && User.Identity.IsAuthenticated;
            if (loggedIn)
                return false;*/

            var result = await _signInManager.PasswordSignInAsync(user.First(), user.Last(), false, false);
            return result.Succeeded;
        }

        // PUT api/caller/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/caller/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
