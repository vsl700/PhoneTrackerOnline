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
        [HttpGet("nIseS2eVZqmEmaWr7ygSkbdLpP83UZgk_WmbhLhH4MQ")]
        public string GetCode()
        {
            return "nIseS2eVZqmEmaWr7ygSkbdLpP83UZgk_WmbhLhH4MQ.sVPdObX68kRGTdgM3t5nkIWyVY2N2qigZ4_AM4Zcj6w";
        }
    }
}
