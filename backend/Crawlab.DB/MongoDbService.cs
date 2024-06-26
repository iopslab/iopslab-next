using MongoDB.Driver;

namespace Crawlab.DB;

public class MongoDbService
{
    public readonly MongoClient Client;

    public MongoDbService()
    {
        var mongoUri = Environment.GetEnvironmentVariable("CRAWLAB_MONGO_URI");
        Client = new MongoClient(mongoUri ?? "mongodb://localhost:27017");
    }

    public IMongoDatabase GetDatabase()
    {
        var dbName = Environment.GetEnvironmentVariable("CRAWLAB_MONGO_DATABASE");
        return Client.GetDatabase(dbName ?? "crawlab");
    }
}