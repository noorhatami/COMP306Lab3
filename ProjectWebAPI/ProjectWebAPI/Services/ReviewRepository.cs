using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using ProjectWebAPI.Models;

namespace ProjectWebAPI.Services
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly IAmazonDynamoDB _dynamoDBClient;
        private const string TableName = "Reviews"; // DynamoDB table name
        private const string AccessKey = "AKIA4MTWJJUZJGSEAV5B";
        private const string SecretKey = "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";

        public ReviewRepository()
        {
            // Create AmazonDynamoDBClient with hardcoded credentials
            AWSCredentials credentials = new BasicAWSCredentials(AccessKey, SecretKey);
            _dynamoDBClient = new AmazonDynamoDBClient(credentials, RegionEndpoint.USEast1);
        }
        public async Task<Reviews> GetReviewAsync(string reviewId)
        {
            var request = new GetItemRequest
            {
                TableName = TableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    { "ReviewId", new AttributeValue { S = reviewId } }
                }
            };

            var response = await _dynamoDBClient.GetItemAsync(request);

            if (response.Item == null)
                return null;

            return MapDynamoDbItemToReview(response.Item);
        }

        public async Task<IEnumerable<Reviews>> GetReviewsAsync()
        {
            var request = new ScanRequest
            {
                TableName = TableName
            };

            var response = await _dynamoDBClient.ScanAsync(request);

            return response.Items.Select(MapDynamoDbItemToReview);
        }

        public async Task AddAsync(Reviews review)
        {
            var doc = new Document();
            doc["ReviewId"] = review.ReviewId;
            doc["Rating"] = review.Rating;
            doc["ReviewTitle"] = review.ReviewTitle;
            doc["Comments"] = review.Comments;
            doc["BookTitle"] = review.BookTitle;
            doc["BookDescription"] = review.BookDescription;
            doc["Recommend"] = review.Recommend;
            doc["SpoilerAlert"] = review.SpoilerAlert;
            doc["ReviewerAge"] = review.ReviewerAge;
            doc["ReviewerName"] = review.ReviewerName;

            var table = Table.LoadTable(_dynamoDBClient, TableName);
            await table.PutItemAsync(doc);
        }

        public async Task DeleteAsync(string reviewId)
        {
            var request = new DeleteItemRequest
            {
                TableName = TableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    { "ReviewId", new AttributeValue { S = reviewId } }
                }
            };

            await _dynamoDBClient.DeleteItemAsync(request);
        }

        public async Task UpdateAsync(string reviewId, Reviews review)
        {
            var doc = new Document();
            doc["ReviewId"] = review.ReviewId;
            doc["Rating"] = review.Rating;
            doc["ReviewTitle"] = review.ReviewTitle;
            doc["Comments"] = review.Comments;
            doc["BookTitle"] = review.BookTitle;
            doc["BookDescription"] = review.BookDescription;
            doc["Recommend"] = review.Recommend;
            doc["SpoilerAlert"] = review.SpoilerAlert;
            doc["ReviewerAge"] = review.ReviewerAge;
            doc["ReviewerName"] = review.ReviewerName;
            var table = Table.LoadTable(_dynamoDBClient, TableName);
            await table.UpdateItemAsync(doc);
        }

        public async Task PatchAsync(string reviewId, Reviews reviewUpdateDTO)
        {
            var request = new UpdateItemRequest
            {
                TableName = TableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    { "ReviewId", new AttributeValue { S = reviewId } }
                },
                AttributeUpdates = new Dictionary<string, AttributeValueUpdate>
                {
                    { "Rating", new AttributeValueUpdate { Action = AttributeAction.PUT, Value = new AttributeValue { N = reviewUpdateDTO.Rating.ToString() } } },
                    { "ReviewTitle", new AttributeValueUpdate { Action = AttributeAction.PUT, Value = new AttributeValue { S = reviewUpdateDTO.ReviewTitle } } },
                    { "Comments", new AttributeValueUpdate { Action = AttributeAction.PUT, Value = new AttributeValue { S = reviewUpdateDTO.Comments } } },
                    { "BookTitle", new AttributeValueUpdate { Action = AttributeAction.PUT, Value = new AttributeValue { S = reviewUpdateDTO.BookTitle } } },
                    { "BookDescription", new AttributeValueUpdate { Action = AttributeAction.PUT, Value = new AttributeValue { S = reviewUpdateDTO.BookDescription } } },
                    { "Recommend", new AttributeValueUpdate { Action = AttributeAction.PUT, Value = new AttributeValue { S = reviewUpdateDTO.Recommend } } },
                    { "SpoilerAlert", new AttributeValueUpdate { Action = AttributeAction.PUT, Value = new AttributeValue { S = reviewUpdateDTO.SpoilerAlert } } },
                    { "ReviewerAge", new AttributeValueUpdate { Action = AttributeAction.PUT, Value = new AttributeValue { S = reviewUpdateDTO.ReviewerAge.ToString() } } },
                    { "ReviewerName", new AttributeValueUpdate { Action = AttributeAction.PUT, Value = new AttributeValue { S = reviewUpdateDTO.ReviewerName } } },

                },
                ReturnValues = ReturnValue.ALL_NEW // Specify what values should be returned after update
            };

            await _dynamoDBClient.UpdateItemAsync(request);
        }

        private Reviews MapDynamoDbItemToReview(Dictionary<string, AttributeValue> item)
        {
            return new Reviews
            {
                ReviewId = item["ReviewId"].S,
                Rating = int.Parse(item["Rating"].N),
                ReviewTitle = item["ReviewTitle"].S,
                Comments = item["Comments"].S,
                BookTitle = item["BookTitle"].S,
                BookDescription = item["BookDescription"].S,
                Recommend = item["Recommend"].S,
                SpoilerAlert = item["SpoilerAlert"].S,
                ReviewerAge = int.Parse(item["ReviewerAge"].N),
                ReviewerName = item["ReviewerName"].S
            };
        }
    }
}
