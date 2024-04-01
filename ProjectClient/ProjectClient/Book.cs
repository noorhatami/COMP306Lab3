using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectClient
{
    public class Book
    {

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

        public override string ToString()
        {
            return $"BookId {BookId}\nTitle {Title}\nISBN {ISBN}\nAuthor {Author}\nPublicationYear {PublicationYear}\nGenre {Genre}\nDescription {Description}\nLanguage {Language}\nRating {Rating}\nUploadedBy {UploadedBy}\n";
        }
    }


}
