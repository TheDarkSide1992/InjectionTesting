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

    public async Task<UserModel> CreateUser(UserModel user)
    {
        var newUser = user.ToDbModel();
        var sql = $@"INSERT INTO users (id, name) 
                VALUES (@id, @name) RETURNING
                id as {nameof(UserDbModel.Id)},
                name as {nameof(UserDbModel.Name)}";
        
        using var conn = _dataSource.OpenConnection();
        
        var result = await conn.QueryFirstAsync<UserDbModel>(sql, new {
            id = Guid.NewGuid(),
            name = newUser.Name
        });
        
        return result.ToModel();
    }

    public async Task<UserModel> UpdateUser(UserModel user)
    {
        var newUser = user.ToDbModel();
        var sql = $@"UPDATE users SET name = @name WHERE id = @id
                RETURNING
                id as {nameof(UserDbModel.Id)},
                name as {nameof(UserDbModel.Name)}";
        
        using var conn = _dataSource.OpenConnection();
        
        var result = await conn.QueryFirstAsync<UserDbModel>(sql, new {
            id = newUser.Id,
            name = newUser.Name
        });

        return result.ToModel();
    }

    public async Task<bool> DeleteUserById(Guid id)
    {
        var sql = $@"DELETE FROM users WHERE id = @id";
        
        using var conn = _dataSource.OpenConnection();
        
        var result = await conn.ExecuteAsync(sql, new { id }) == 1;

        return result;
    }
}