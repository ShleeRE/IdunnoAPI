using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdunnoAPI.Models
{
    [Table("Users")]
    public class User
    {

        [Required] public int UserID { get; set; }

        [Required] public string Username { get; set; }

        [Required] public string Password { get; set; }

        [Required] public string Role { get; private set; } = "User";
    }
}
