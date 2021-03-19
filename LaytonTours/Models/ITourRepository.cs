using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaytonTours.Models
{
    //Create ITourRepository interface
    public interface ITourRepository
    {
        IQueryable<Appointment> Appointments { get; }
    }
}
