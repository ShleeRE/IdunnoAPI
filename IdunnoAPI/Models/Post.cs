using System.ComponentModel.DataAnnotations;

namespace IdunnoAPI.Models
{
    public class Post
    {
        [Required]
        public int PostID { get; set; }
        [Required]
        public int UserID { get; set; }
        public string PostDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd H:mm");
        [Required]
        public string PostTitle { get; set; }
        [Required]
        public string PostDescription { get; set; }

        public string ImagePath { get; set; }

    }
}
