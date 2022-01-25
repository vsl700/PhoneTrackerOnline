using System.ComponentModel.DataAnnotations;

namespace PhoneTrackerOnline.Models
{
    public class Contact
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        public int UserID { get; set; }
    }
}
