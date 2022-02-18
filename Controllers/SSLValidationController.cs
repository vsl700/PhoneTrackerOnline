using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneTrackerOnline.Controllers
{
    [Route(".well-known/pki-validation")]
    [ApiController]
    public class SSLValidationController : ControllerBase
    {
        [HttpGet("9FF99F4F6B14F6BDECAD20D246760F47.txt")]
        public string GetCode()
        {
            return "4B7FCDC970507306271288553E2447003887A0BB1C1A87E74BDDE8366CA66E18\ncomodoca.com\naf0245600ebc809";
        }
    }
}
