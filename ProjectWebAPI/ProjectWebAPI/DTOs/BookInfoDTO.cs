namespace ProjectWebAPI.DTOs
{
    public class BookInfoDTO 
    {
        public string BookId { get; set; }
        public string Title { get; set; }
        public int ISBN { get; set; }
        public string Author { get; set; }
        public string PublicationYear { get; set; }
        public string Genre { get; set; }
        public string Language { get; set; }
    }
}
