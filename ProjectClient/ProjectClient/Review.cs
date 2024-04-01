using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ProjectClient
{
    public class Review
    {
        public string ReviewId { get; set; }
        public int Rating { get; set; }
        public string ReviewTitle { get; set; }
        public string Comments { get; set; }
        public string BookTitle { get; set; }
        public string BookDescription { get; set; }
        public string SpoilerAlert { get; set; }
        public string Recommend { get; set; }
        public string ReviewerName { get; set; }
        public int ReviewerAge { get; set; }

        public override string ToString()
        {
            return $"ReviewId {ReviewId}\nRating {Rating}\nReviewTitle {ReviewTitle}\nComments {Comments}\nBookTitle {BookTitle}\nBookDescription {BookDescription}\nSpoilerAlert {SpoilerAlert}\nRecommend {Recommend}\nReviewerName {ReviewerName}\nReviewerAge {ReviewerAge}\n";
        }
    }
}
