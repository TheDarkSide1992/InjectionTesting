using Infastructure.Models;

namespace Infastructure;

public interface IMongoUserRepo
{
    public Task<List<User>> GetUserByName(string Name);
    public Task<User> UpdateUser(User user);
    public Task<User> CreateUser(User user);
    public Task<bool> DeleteUser(UserId id);
}