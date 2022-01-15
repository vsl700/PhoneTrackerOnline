using System.ComponentModel.DataAnnotations;

namespace PhoneTrackerOnline.Models
{
    public class Location
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public int MarkerColor { get; set; }

        [Required]
        public string TimeTaken { get; set; }

        public int TargetPhoneID { get; set; }
    }
}
