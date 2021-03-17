using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaytonTours.Models
{
    public class EFTourRepository : ITourRepository
    {
        //create ToursDbContext attribute
        private ToursDbContext _context;
        //Class constructor
        public EFTourRepository(ToursDbContext context)
        {
            _context = context;
        }

        public IQueryable<Appointment> Appointments => _context.Appointments;
    }
}
