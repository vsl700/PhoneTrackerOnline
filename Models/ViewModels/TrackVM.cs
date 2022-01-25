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

        public int PhoneID { get; set; } // id == 0 means new, id > 0 means edit an existing one

        [Required]
        public string PhoneName { get; set; }

        public int PhoneContactID { get; set; }

        public bool Delete { get; set; }
    }
}
