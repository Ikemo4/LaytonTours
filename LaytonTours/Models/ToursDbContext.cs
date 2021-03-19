using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaytonTours.Models
{
    //DbContext class for database purposes
    public class ToursDbContext : DbContext
    {
        public ToursDbContext(DbContextOptions<ToursDbContext> options) : base(options)
        {

        }

        public DbSet<Appointment> Appointments { get; set; }
    }
}
