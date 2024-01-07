using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace project_scoo.Models
{
    public class Bus
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CapName { get; set; }
        [Required]
        public int NumOfSeats { get; set; }
        
        [ForeignKey("TripId")]
        public virtual Trip Trip { get; set; }

        [ForeignKey("AdminId")]
        public Admin Admin { get; set; }
    }
}
