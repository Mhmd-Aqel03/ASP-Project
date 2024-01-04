using Microsoft.EntityFrameworkCore;
using project_scoo.Models;

namespace project_scoo.Data
{
    public class SystemDbContext : DbContext
    {
        public SystemDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Bus> Busses { get; set; }
        public DbSet<Passenger> Passenegrs { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Passenger_trips> Passengers_Trips { get; set; }
    }
}
