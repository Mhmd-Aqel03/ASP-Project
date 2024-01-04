using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace project_scoo.Models
{
    [Index(nameof(Trip.BusNumber), IsUnique = true)]
    public class Trip
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int NumOfBusses {  get; set; }
        [Required]
        public string Destination { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        public ICollection<Bus> Busses { get; set; }
        public ICollection<Passenger_trips> Pasenger_Trip {get;set; }
    }
}
