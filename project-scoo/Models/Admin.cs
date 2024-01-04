using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace project_scoo.Models
{
    [Index(nameof(Passenger.Username), IsUnique = true)]
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Username {  get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FName { get; set; }
        [Required]  
        public string LName { get; set; }
    }
}
