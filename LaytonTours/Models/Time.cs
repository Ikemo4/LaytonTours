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
        public static IEnumerable<Time> Times { get; set; }
        //getAvailableTimes method to display times for date
        public IEnumerable<Time> GetAvailableTimes(string Date)
        {
            return (IEnumerable<Time> times
                .Select(x => x.ScheduledTime)
                .Where(x => x.Date == Date));
        }
    }
}
