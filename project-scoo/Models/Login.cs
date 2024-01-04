using System.ComponentModel.DataAnnotations;

namespace FootballGame.Models
{
    public class Login
    {
        [Required(ErrorMessage ="Please fill the username")]
        public string Username { set; get; }
        [Required(ErrorMessage = "*")]

        public string Password { set; get; }
    }
}
