using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;

namespace ProjectWebAPI.Services
{
    public class DynamoDBInitializer
    {
        private readonly IAmazonDynamoDB _dynamoDBClient;
        private readonly string _accessKey = "AKIA4MTWJJUZJGSEAV5B";
        private readonly string _secretKey = "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";
        private readonly RegionEndpoint _region = RegionEndpoint.USEast1;

        public DynamoDBInitializer()
        {
            AWSCredentials credentials = new BasicAWSCredentials(_accessKey, _secretKey);
            _dynamoDBClient = new AmazonDynamoDBClient(credentials, _region);
        }

        public async Task CreateBooksTableAsync()
        {
            await CreateTableAsync("Books", "BookId");
        }

        public async Task CreateReviewsTableAsync()
        {
            await CreateTableAsync("Reviews", "ReviewId");
        }

        private async Task CreateTableAsync(string tableName, string primaryKey)
        {
            // Check if the table already exists
            if (!await TableExists(tableName))
            {
                CreateTableRequest request = new CreateTableRequest
                {
                    TableName = tableName,
                    AttributeDefinitions = new List<AttributeDefinition>
                    {
                        new AttributeDefinition { AttributeName = primaryKey, AttributeType = "S" } // String type
                    },
                    KeySchema = new List<KeySchemaElement>
                    {
                        new KeySchemaElement { AttributeName = primaryKey, KeyType = "HASH" } // HASH = Partition key
                    },
                    ProvisionedThroughput = new ProvisionedThroughput
                    {
                        ReadCapacityUnits = 10,
                        WriteCapacityUnits = 10
                    }
                };

                await _dynamoDBClient.CreateTableAsync(request);
            }
        }

        private async Task<bool> TableExists(string tableName)
        {
            try
            {
                var response = await _dynamoDBClient.DescribeTableAsync(tableName);
                return response.Table != null;
            }
            catch (ResourceNotFoundException)
            {
                return false;
            }
        }
    }
}
