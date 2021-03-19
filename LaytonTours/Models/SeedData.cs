using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaytonTours.Models
{
    public class SeedData
    {
        //Ensure populated method to automatically migrate pending migrations
        //and populate database with appointment if none exist
        public static void EnsurePopulated(IApplicationBuilder application)
        {
            ToursDbContext context = application.ApplicationServices.CreateScope()
                .ServiceProvider.GetRequiredService<ToursDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (!context.Appointments.Any())
            {
                context.Appointments.AddRange(
                    new Appointment
                    {
                        GroupName = "Cole Family",
                        GroupSize = 6,
                        Email = "thecoles@gmail.com",
                        Phone = "801-555-5555"
                    });
            }
        }
    }
}
