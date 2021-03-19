using LaytonTours.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace LaytonTours.Controllers
{
    public class HomeController : Controller
    {

        // Intialize variables we will need like the logger, the repository, our time list, and the context
        private readonly ILogger<HomeController> _logger;
        private List<Time> times = new List<Time>();
        private ITourRepository _repository;
        private ToursDbContext _context;


        // When a new homecontroller is created, pass in the needed variables
        public HomeController(ILogger<HomeController> logger, ITourRepository repository, ToursDbContext context)
        {
            _logger = logger;
            _repository = repository;
            _context = context;
             
            

        // Used to generate times
        DateTime currentDate = DateTime.Now;

            
            int objectsCreated = 0;
          
            // This will loop through 7 days and make appropiate timeslots over the 12 hours starting at 8am and the the last timeslot at 7pm
            for (int days = 0; days < 7; days++)
            {
                int startTime = 8;
                string timetype = "AM";
                for (int hours = 0; hours < 12; hours++)
                {
                    Time newTime = new Time();
                    newTime.Date = currentDate.AddDays(days).ToString("dd/MM/yyyy");
                    newTime.ScheduledTime = (startTime + hours).ToString() + ":00 " + timetype;
                    // Use to swap from AM to PM
                    if ((startTime + hours) == 12)
                    {
                        timetype = "PM";
                        newTime.ScheduledTime = (startTime + hours).ToString() + ":00 " + timetype;
                        startTime = 0-hours;
                    }
                    newTime.TimeID = objectsCreated;
                    objectsCreated++;
                    times.Add(newTime);
                }
            }

        }

        
        public IActionResult Index()
        {
            // Was used for testing purposes
            // ViewBag.Times = times;
            return View();
        }

        public IActionResult ViewAppointments()
        {
            // Create a new iterable enumeration of appointments, ordered by AppointmentId
            IEnumerable<Appointment> appointments = _repository.Appointments.OrderBy(a => a.AppointmentId);
          


            return View(appointments);
        }

        [HttpGet("SignUp")]
        public IActionResult SignUp()
        {
            
            

            // Iterate through and make sure any appointments in these times exist in the database, otherwise remove them 
            foreach (Time t in times)
            {
                if (!_repository.Appointments.Any(a => a.AppointmentId == t.AppointmentID))
                {
                    t.AppointmentID = null;
                }
            }

            // Since we arent using a database for the times, we need to update them from the appointment database
            foreach (Appointment a in _repository.Appointments)
            {
                Time time = times.Find(i => i.TimeID == a.TimeID);
                if (time != null)
                {
                    time.AppointmentID = a.AppointmentId;
                }
            }


            // When going to the intial sign up page, make sure the viewbag has the times list
            ViewBag.Times = times;
            
            return View();
        }

         [HttpGet("signup/{timeID:int}")]
        public IActionResult Form(int timeID)
        {
            // Get time object from TimeID
            Time time = times.Find(i => i.TimeID == timeID);
            // Check that this time is empty, otherwise send them back to the view appointments page.
            if (time!=null && time.AppointmentID==null) { 
            // Create a new apppointment and hook up the timeId to match the appointmentID
            // Ideally we will want to port this to the database to handle once we create a database object for time
            Appointment appointment = new Appointment();
            appointment.TimeID = timeID;
            appointment.AppointmentId = getNextId();
            
            time.AppointmentID = appointment.AppointmentId;

                ViewBag.Times = times;
                ViewBag.AppointmentTime = time;
                return View(appointment);
            }

            return RedirectToAction("SignUp");
        }

        // Method to get the next PrimaryKey id
        private int getNextId()
        {
            var nextEntities = _repository.Appointments.OrderBy(d => d.AppointmentId)
         .Where(l => l.AppointmentId > 0)
         .Take(1).SingleOrDefault();
            return nextEntities != null ? nextEntities.AppointmentId : 0;

        }

        [HttpPost("signup/{timeID:int}")]
        public IActionResult Form(Appointment appointment)
        {

          
            // If this appointment matches the ModelState, push to database
            if (ModelState.IsValid)
            {

                _context.Add(appointment);
                _context.SaveChanges();

                return RedirectToAction("ViewAppointments");
            }
            // Else return user to form page to fix errors
            return View(appointment);
          

        }
    }
}

