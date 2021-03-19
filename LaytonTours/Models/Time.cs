using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LaytonTours.Models
{
    //Time Class with neccessary attributes
    public class Time
    {
        [Key]
        public int TimeID { get; set; }
        //appointmentID is nullable. If null, no appointment has been assigned to this time
        public int? AppointmentID { get; set; }
        public string ScheduledTime { get; set; }
        public string Date { get; set; }
        public static IEnumerable<Time> Times { get; set; }        
    }
}
