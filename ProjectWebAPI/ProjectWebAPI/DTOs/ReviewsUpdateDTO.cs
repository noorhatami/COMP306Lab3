using ProjectWebAPI.Models;
namespace ProjectWebAPI.DTOs
{
    public class ReviewsUpdateDTO
    {
        public int Rating { get; set; }
        public string ReviewTitle { get; set; }
        public string Comments { get; set; }
        public string BookTitle { get; set; }
        public string BookDescription { get; set; }
        public string SpoilerAlert { get; set; }
        public string Recommend { get; set; }
        public string ReviewerName { get; set; }
        public int ReviewerAge { get; set; }
    }
}
