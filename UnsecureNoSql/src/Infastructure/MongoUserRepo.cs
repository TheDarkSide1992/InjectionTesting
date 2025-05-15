using Infastructure.Mappers;
using Infastructure.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infastructure;

public class MongoUserRepo : IMongoUserRepo
{
    private readonly MongoContext _mongoContext;

    public MongoUserRepo(MongoContext mongoContext)
    {
        _mongoContext = mongoContext;
    }
    
    
    public async Task<List<User>> GetUserByName(string name)
    {
        var filter = new BsonDocument
        {
            {"Name", name}
        };
        var user = await _mongoContext._users.Find<UserDbModel>(filter).ToListAsync();
        var userList = new List<User>();
        foreach (var userDbModel in user)
        {
            Console.WriteLine(userDbModel.Id);
            userList.Add(userDbModel.ToDomain());
        }

        return userList;
    }

    public async Task<User> UpdateUser(User user)
    {
        try
        {
            var filter = Builders<UserDbModel>.Filter.Eq("Id", user.Id);
            var update = Builders<UserDbModel>.Update.Set("Name", user.Name);
            await _mongoContext._users.UpdateOneAsync(filter, update);
            return user;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<User> CreateUser(User user)
    {
        try
        { 
            await _mongoContext._users.InsertOneAsync(user.ToDbModel());
            return user;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    

    public async Task<bool> DeleteUser(UserId id)
    {
        try
        {
            var result = await _mongoContext._users.DeleteOneAsync(u => u.Id == id.Value);
            return result.DeletedCount > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}