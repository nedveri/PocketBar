using backend.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace backend.Services;

public class MongoDbClient
{
    private static IMongoDatabase _db;
    private static readonly Lazy<MongoDbClient> _instance = new(() => new MongoDbClient());

    public static MongoDbClient Instance => _instance.Value;

    private MongoDbClient()
    {
        var connectionString = "mongodb+srv://mkoval706_db_user:cbrPS2hYEiziWcmI@database.nettexh.mongodb.net/PocketBarDB?appName=DataBase";
        var client = new MongoClient(connectionString);
        _db = client.GetDatabase("PocketBarDB");
    }

    public IMongoCollection<T> GetCollection<T>(string name) => _db.GetCollection<T>(name);
}