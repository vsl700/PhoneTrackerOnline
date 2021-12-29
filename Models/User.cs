using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhoneTrackerOnline.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Username { get; set; }

        public virtual ICollection<TargetPhone> TrackedPhones { get; set; }

        public virtual ICollection<Contact> ContactsList { get; set; }
    }
}
