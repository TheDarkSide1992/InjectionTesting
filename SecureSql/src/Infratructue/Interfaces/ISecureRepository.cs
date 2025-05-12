using Infratructue.Models;

namespace Api.Controllers;

public interface ISecureRepository
{
    Task<UserModel> GetUserByName(string name);
    
    Task<UserModel> CreateUser(UserModel user);
    
    Task<UserModel> UpdateUser(UserModel user);
    
    Task<bool> DeleteUserById(Guid id);
    
    
}