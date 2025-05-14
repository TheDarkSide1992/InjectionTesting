using Infratructue.Models;

namespace Api.Controllers;

public interface ISecureRepository
{
    Task<IEnumerable<UserModel>> GetUsersByName(string name);

    Task<IEnumerable<UserModel>> GetUsers();
    
    Task<UserModel> CreateUser(UserModel user);
    
    Task<UserModel> UpdateUser(UserModel user);
    
    Task<bool> DeleteUserById(Guid id);
    
    
}