namespace ProjectWebAPI.DTOs
{
    public class BookUpdateDTO
    {
        public string Title { get; set; }
        public int ISBN { get; set; }
        public string Author { get; set; }
        public string PublicationYear { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public int Rating { get; set; }
        public string UploadedBy { get; set; }
    }
}
