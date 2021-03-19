using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LaytonTours.Models
{
    //Appointment class with necessary attributes
    public class Appointment
    {
        [Key]
        [Required]
        public int AppointmentId { get; set; }
        [Required]
        public string GroupName { get; set; }
        [Required]
        public int GroupSize { get; set; }
        [Required]
        public string Email { get; set; }
        //Use Regex to require user to follow standard phone number formatting
        [RegularExpression(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}")]
        public string Phone { get; set; }
        public int TimeID { get; set; }
    }
}
