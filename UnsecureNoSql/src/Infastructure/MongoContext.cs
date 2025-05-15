using Infastructure.Models;
using MongoDB.Driver;

namespace Infastructure;

public class MongoContext
{
    private readonly IMongoDatabase _database;
    public readonly IMongoCollection<UserDbModel> _users;

    public MongoContext(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
        _users = _database.GetCollection<UserDbModel>("Users");
    }
}