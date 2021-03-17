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
        private ITourRepository _repository;
        private readonly ILogger<HomeController> _logger;
        private List<Time> times = new List<Time>();



        public HomeController(ILogger<HomeController> logger, ITourRepository repository)
        {
            _logger = logger;
            _repository = repository;
             
            

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
            ViewBag.Times = times;
            return View();
        }

        [HttpGet("SignUp")]
        public IActionResult SignUp()
        {
            ViewBag.Times = times;
            // Uncomment once code is ready - return View(Time.getAvailableTimes());
            return View();
        }

        [HttpPost("SignUp")]
        public IActionResult SignUp(Appointment appointment)
        {
            ViewBag.Times = times;
            if (ModelState.IsValid)
            {

                return View("ViewAppointments");
            }

            return View();
            // Uncomment once code is ready - return View(Time.getAvailableTimes());
        }
    }
}

