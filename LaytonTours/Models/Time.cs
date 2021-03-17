using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LaytonTours.Models
{
    public class Time
    {
        [Key]
        public int TimeID { get; set; }
        //appointmentID
        public int? AppointmentID { get; set; }
        public string ScheduledTime { get; set; }
        public string Date { get; set; }
        //getAvailableTimes method
    }
}
