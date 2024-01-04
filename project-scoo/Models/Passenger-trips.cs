using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace project_scoo.Models
{
    public class Passenger_trips
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("PassengerID")]
        public Passenger Passenegr { get; set; }
        [ForeignKey("TripID")]
        public Trip Trip { get; set; }                  
    }
}
