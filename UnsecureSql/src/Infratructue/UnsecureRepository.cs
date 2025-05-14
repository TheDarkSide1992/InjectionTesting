using Dapper;
using Infratructue.Models;
using Npgsql;

namespace Infratructue;

public class UnsecureRepository
{
    private readonly NpgsqlDataSource _dataSource;
    
    public UnsecureRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }
    
    public async Task<string> GetUsersByName(string name)
    {
        var sql = @$"SELECT * FROM users WHERE name = '{name}';" ;
        
        using var conn = _dataSource.OpenConnection();
        return (await conn.QueryAsync(sql)).ToString();
    }
    
    public async Task<string> GetUsers()
    {
        var sql = "SELECT * FROM users";
        
        using var conn = _dataSource.OpenConnection();
        return (await conn.QueryAsync(sql)).ToString();
    }

    public async Task<string> CreateUser(UserModel user)
    {
        var sql = @$"INSERT INTO users (userId, name) VALUES (  '{Guid.NewGuid()}', '{user.Name}') RETURNING *";
        
        using var conn = _dataSource.OpenConnection();
        
        return await conn.QueryFirstAsync(sql);
    }

    public async Task<string> UpdateUser(UserModel user)
    {
        var sql = @$"UPDATE users SET name = '{user.Name}' WHERE userId = '{user.Id}' RETURNING *";
        
        using var conn = _dataSource.OpenConnection();
        
        return await conn.QueryFirstAsync(sql);
    }

    public async Task<bool> DeleteUserById(Guid id)
    {
        var sql = $@"DELETE FROM users WHERE userId = '{id}'";
        
        using var conn = _dataSource.OpenConnection();
        
        var result = await conn.ExecuteAsync(sql) == 1;

        return result;
    }
}