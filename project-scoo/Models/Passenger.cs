using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace project_scoo.Models
{
    [Index(nameof(Passenger.Username), IsUnique = true)]
    public class Passenger
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public bool gender { get; set; }
        public ICollection<Passenger_trips> Pasenger_Trip {get;set; }
    }
}
