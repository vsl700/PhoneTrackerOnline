using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneTrackerOnline.Models.ViewModels
{
    public class TrackVM
    {
        public User Caller { get; set; }

        public Dictionary<TargetPhone, string> ContactStrings { get; set; }

        public int PhoneID { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        [StringLength(20, ErrorMessage = "The maximum length of a phone name is 20!")]
        public string PhoneName { get; set; }

        public int PhoneContactID { get; set; }
    }
}
