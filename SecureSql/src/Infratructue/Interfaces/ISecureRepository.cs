using Infratructue.Models;

namespace Api.Controllers;

public interface IUnsecureRepository
{
    Task<UserModel> GetUserByName(string name);
    
    Task<UserModel> CreateUser(UserModel user);
    
    Task<bool> UpdateUser(UserModel user);
    
    Task<bool> DeleteUserById(Guid id);
    
    
}