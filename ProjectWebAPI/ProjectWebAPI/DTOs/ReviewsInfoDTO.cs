namespace ProjectWebAPI.DTOs
{
    public class ReviewsInfoDTO
    {
        public string ReviewId { get; set; }
        public int Rating { get; set; }
        public string ReviewTitle { get; set; }
        public string Comments { get; set; }
        public string BookTitle { get; set; }
        public string BookDescription { get; set; }
    }
}
