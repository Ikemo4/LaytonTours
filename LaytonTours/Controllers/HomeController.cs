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
        
        private readonly ILogger<HomeController> _logger;
        private List<Time> times = new List<Time>();
        private ITourRepository _repository;
        private ToursDbContext _context;



        public HomeController(ILogger<HomeController> logger, ITourRepository repository, ToursDbContext context)
        {
            _logger = logger;
            _repository = repository;
            _context = context;
             
            

        // Used to generate times
        DateTime currentDate = DateTime.Now;
            int startTime = 4;
            int objectsCreated = 0;

            for (int days = 0; days < 7; days++)
            {
                for (int hours = 0; hours < 3; hours++)
                {
                    Time newTime = new Time();
                    newTime.Date = currentDate.AddDays(days).ToString("dd/MM/yyyy");
                    newTime.ScheduledTime = (startTime + hours).ToString() + ":00 PM";
                    newTime.TimeID = objectsCreated;
                    objectsCreated++;
                    times.Add(newTime);
                }
            }
            
        }

        public IActionResult Index()
        {
            ViewBag.Times = times;
            return View();
        }

        public IActionResult ViewAppointments()
        {
            
           // Iterate through and make sure any appointments in these times exist in the database, otherwise remove them 
           foreach (Time t in times)
            {
                if (!_repository.Appointments.Any(a => a.AppointmentId == t.AppointmentID)) {
                    t.AppointmentID = null;
                }
            }

           ViewBag.Times = times;

            return View();
        }

        [HttpGet("SignUp")]
        [HttpGet("signup/{timeID:int}")]
        public IActionResult SignUp(int timeID)
        {
            // Get time object from TimeID
            Time time = times.Find(i => i.TimeID == timeID);
            // Check that this time is empty, otherwise send them back to the view appointments page.
            if (time!=null && time.AppointmentID==null) { 
            // Create a new apppointment and hook up the timeId to match the appointmentID
            // Ideally we will want to port this to the database to handle once we create a database object  for time
            Appointment appointment = new Appointment();
            appointment.TimeID = timeID;
            appointment.AppointmentId = getNextId();
            
            time.AppointmentID = appointment.AppointmentId;
            

            return View(appointment);
            }
            return View("ViewAppointments");
        }

        // Method to get the next PrimaryKey id
        private int getNextId()
        {
            var nextEntities = _repository.Appointments.OrderBy(d => d.AppointmentId)
         .Where(l => l.AppointmentId > 0)
         .Take(1).SingleOrDefault();
            return nextEntities != null ? nextEntities.AppointmentId : 0;
        }

        [HttpPost("SignUp")]
        public IActionResult SignUp(Appointment appointment)
        {
            
            // If this appointment matches the ModelState, push to database
            if (ModelState.IsValid)
            {

                _context.Add(appointment);

                return View("ViewAppointments");
            }
            // Else return user to form page to fix errors
            return View(appointment);
          
        }
    }
}

