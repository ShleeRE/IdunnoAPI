using System.ComponentModel.DataAnnotations;

namespace IdunnoAPI.Models
{
    public class User
    {
        [Required]
        public int UserID { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public string Role { get; set; }
    }
}
