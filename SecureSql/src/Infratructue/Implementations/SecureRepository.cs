using Api.Controllers;
using Dapper;
using Infratructue.Mappers;
using Infratructue.Models;
using Npgsql;

public class UnsecureRepository : IUnsecureRepository
{
    private readonly NpgsqlDataSource _dataSource;
    
    public UnsecureRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public async Task<UserModel> GetUserByName(string name)
    {
        var sql = $@"SELECT 
            id as {nameof(UserDbModel.Id)},
            name as {nameof(UserDbModel.Name)}
            FROM users WHERE name = @name";
        
        using var conn = _dataSource.OpenConnection();
        var result = conn.QueryFirst<UserDbModel>(sql, new { name });
        return result.ToModel();
    }

    public Task<UserModel> CreateUser(UserModel user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateUser(UserModel user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteUserById(Guid id)
    {
        throw new NotImplementedException();
    }
}