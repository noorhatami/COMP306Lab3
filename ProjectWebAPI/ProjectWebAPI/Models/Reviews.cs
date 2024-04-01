using Amazon.DynamoDBv2.DataModel;
using ProjectWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace ProjectWebAPI.Models
{
    [DynamoDBTable("Reviews")]
    public class Reviews
    {
        [DynamoDBHashKey]
        public string ReviewId { get; set; }
        public int Rating { get; set; }
        public string ReviewTitle { get; set; }
        public string Comments { get; set; }
        public string BookTitle { get; set; }
        public string BookDescription { get; set; }
        public string Recommend { get; set; }
        public string SpoilerAlert { get; set; }
        public int ReviewerAge { get; set; }
        public string ReviewerName { get; set; }
    }
}