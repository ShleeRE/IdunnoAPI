namespace IdunnoAPI.Models
{
    public class Post
    {
        public int PostID { get; set; }
        public int UserID { get; set; }
        public string PostDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd H:mm");
        public string PostTitle { get; set; } 
        public string PostDescription { get; set; }

        public string ImagePath { get; set; }

    }
}
