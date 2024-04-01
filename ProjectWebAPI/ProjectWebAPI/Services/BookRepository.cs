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
    public class BookRepository : IBookRepository
    {
        private readonly IAmazonDynamoDB _dynamoDBClient;
        private const string TableName = "Books"; // DynamoDB table name
        private const string AccessKey = "AKIA4MTWJJUZJGSEAV5B";
        private const string SecretKey = "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";

        public BookRepository()
        {
            // Create AmazonDynamoDBClient with hardcoded credentials
            AWSCredentials credentials = new BasicAWSCredentials(AccessKey, SecretKey);
            _dynamoDBClient = new AmazonDynamoDBClient(credentials, RegionEndpoint.USEast1);
        }

        public async Task<Books> GetBookAsync(string bookId)
        {
            var request = new GetItemRequest
            {
                TableName = TableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    { "BookId", new AttributeValue { S = bookId } }
                }
            };

            var response = await _dynamoDBClient.GetItemAsync(request);

            if (response.Item == null)
                return null;

            return MapDynamoDbItemToBooks(response.Item);
        }

        public async Task<IEnumerable<Books>> GetBooksAsync()
        {
            var request = new ScanRequest
            {
                TableName = TableName
            };

            var response = await _dynamoDBClient.ScanAsync(request);

            return response.Items.Select(MapDynamoDbItemToBooks);
        }

        public async Task AddAsync(Books books)
        {
            var doc = new Document();
            doc["BookId"] = books.BookId;
            doc["Title"] = books.Title;
            doc["ISBN"] = books.ISBN;
            doc["Author"] = books.Author;
            doc["PublicationYear"] = books.PublicationYear;
            doc["Genre"] = books.Genre;
            doc["Description"] = books.Description;
            doc["Language"] = books.Language;
            doc["Rating"] = books.Rating;
            doc["UploadedBy"] = books.UploadedBy;

            var table = Table.LoadTable(_dynamoDBClient, TableName);
            await table.PutItemAsync(doc);
        }

        public async Task DeleteAsync(string bookId)
        {
            var request = new DeleteItemRequest
            {
                TableName = TableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    { "BookId", new AttributeValue { S = bookId } }
                }
            };

            await _dynamoDBClient.DeleteItemAsync(request);
        }

        public async Task UpdateAsync(string bookId, Books books)
        {
            var request = new UpdateItemRequest
            {
                TableName = TableName,
                Key = new Dictionary<string, AttributeValue>
        {
            { "BookId", new AttributeValue { S = bookId } }
        },
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
        {
            { ":title", new AttributeValue { S = books.Title } },
            { ":isbn", new AttributeValue { N = books.ISBN.ToString() } },
            { ":author", new AttributeValue { S = books.Author } },
            { ":publicationYear", new AttributeValue { S = books.PublicationYear } },
            { ":genre", new AttributeValue { S = books.Genre } },
            { ":description", new AttributeValue { S = books.Description } },
            { ":lang", new AttributeValue { S = books.Language } }, // Changed attribute name to "lang"
            { ":rating", new AttributeValue { N = books.Rating.ToString() } },
            { ":uploadedBy", new AttributeValue { S = books.UploadedBy } }
        },
                ExpressionAttributeNames = new Dictionary<string, string>
        {
            { "#lang", "Language" } // Define an alternative name for the "Language" attribute
        },
                UpdateExpression = "SET Title = :title, ISBN = :isbn, Author = :author, PublicationYear = :publicationYear, Genre = :genre, Description = :description, #lang = :lang, Rating = :rating, UploadedBy = :uploadedBy", // Use the alternative name in the UpdateExpression
                ReturnValues = ReturnValue.ALL_NEW // Specify what values should be returned after update
            };

            await _dynamoDBClient.UpdateItemAsync(request);
        }

        public async Task PatchAsync(string bookId, Books bookUpdateDTO)
        {
            var request = new UpdateItemRequest
            {
                TableName = TableName,
                Key = new Dictionary<string, AttributeValue>
        {
            { "BookId", new AttributeValue { S = bookId } }
        },
                AttributeUpdates = new Dictionary<string, AttributeValueUpdate>
        {
            { "Title", new AttributeValueUpdate { Action = AttributeAction.PUT, Value = new AttributeValue { S = bookUpdateDTO.Title } } },
            { "ISBN", new AttributeValueUpdate { Action = AttributeAction.PUT, Value = new AttributeValue { N = bookUpdateDTO.ISBN.ToString() } } },
            { "Author", new AttributeValueUpdate { Action = AttributeAction.PUT, Value = new AttributeValue { S = bookUpdateDTO.Author } } },
            { "PublicationYear", new AttributeValueUpdate { Action = AttributeAction.PUT, Value = new AttributeValue { S = bookUpdateDTO.PublicationYear } } },
            { "Genre", new AttributeValueUpdate { Action = AttributeAction.PUT, Value = new AttributeValue { S = bookUpdateDTO.Genre } } },
            { "Language", new AttributeValueUpdate { Action = AttributeAction.PUT, Value = new AttributeValue { S = bookUpdateDTO.Language } } }, // Keep the attribute name as "Language" for PATCH operation
        },
                ReturnValues = ReturnValue.ALL_NEW // Specify what values should be returned after update
            };

            await _dynamoDBClient.UpdateItemAsync(request);
        }

        private Books MapDynamoDbItemToBooks(Dictionary<string, AttributeValue> item)
        {
            return new Books
            {
                BookId = item["BookId"].S,
                Title = item["Title"].S,
                ISBN = int.Parse(item["ISBN"].N),
                Author = item["Author"].S,
                PublicationYear = item["PublicationYear"].S,
                Genre = item["Genre"].S,
                Description = item["Description"].S,
                Language = item["Language"].S,
                Rating = int.Parse(item["Rating"].N),
                UploadedBy = item["UploadedBy"].S
            };
        }
    }
}
