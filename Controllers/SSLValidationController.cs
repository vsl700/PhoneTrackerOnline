using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneTrackerOnline.Controllers
{
    [Route(".well-known/acme-challenge")]
    [ApiController]
    public class SSLValidationController : ControllerBase
    {
        [HttpGet("hjPw6v-a3lMxx77vMMSyHHkJyEC7_tSjj-e9AbGfuWs")]
        public string GetCode()
        {
            return "hjPw6v-a3lMxx77vMMSyHHkJyEC7_tSjj-e9AbGfuWs.fkhTTWimpoHlneONE2jYn2LOC45TkVmaQTWQnz7KoZA";
        }
    }
}
