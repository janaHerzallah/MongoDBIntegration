using MongoDB.Driver;

namespace MongoDBIntegration.Data
{
    public class MongoDbService
    {
        private readonly IMongoDatabase _database;
        private readonly IConfiguration _configuration;
        public MongoDbService(IConfiguration configuration)
        {
            configuration = configuration;
           
            var connectionString = configuration.GetConnectionString("DbConnection");
            var mongoUrl =  MongoUrl.Create(connectionString);

            var mongoClient = new MongoClient(mongoUrl);
            _database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
        }

        public IMongoDatabase? Database => _database;
        



    }
}
