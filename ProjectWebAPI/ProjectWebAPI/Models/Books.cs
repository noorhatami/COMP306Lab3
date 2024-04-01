using Amazon.DynamoDBv2.DataModel;

namespace ProjectWebAPI.Models
{
    [DynamoDBTable("Books")]
    public class Books
    {
        [DynamoDBHashKey]
        public string BookId { get; set; }
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