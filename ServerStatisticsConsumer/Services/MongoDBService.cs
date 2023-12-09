using MongoDB.Driver;

namespace ServerStatisticsConsumer.Services
{
    public class MongoDBService : IMongoDB
    {
        private readonly IMongoCollection<ServerStatistics> _serverCollection;
        public async Task Insert(ServerStatistics serverStatistics)
        {
            Console.WriteLine(serverStatistics);
            await _serverCollection.InsertOneAsync(serverStatistics);
        }

        public MongoDBService(string connection , string databaseName , string collectionName)
        {
            var client = new MongoClient(connection);
            var database = client.GetDatabase(databaseName);
            _serverCollection = database.GetCollection<ServerStatistics>(collectionName);      
        }
    }
}