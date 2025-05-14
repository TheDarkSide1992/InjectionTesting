using Api.Controllers;
using Dapper;
using Infratructue.Mappers;
using Infratructue.Models;
using Npgsql;

public class SecureRepository : ISecureRepository
{
    private readonly NpgsqlDataSource _dataSource;
    
    public SecureRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public async Task<IEnumerable<UserModel>> GetUsersByName(string name)
    {
        var sql = $@"SELECT 
            userId as {nameof(UserDbModel.Id)},
            name as {nameof(UserDbModel.Name)}
            FROM users WHERE name = @name";
        
        using var conn = _dataSource.OpenConnection();
        var result = await conn.QueryAsync<UserDbModel>(sql, new { name });
        return result.Select(x => x.ToModel());
    }
    
    
    public async Task<IEnumerable<UserModel>> GetUsers()
    {
        var sql = $@"SELECT 
            userId as {nameof(UserDbModel.Id)},
            name as {nameof(UserDbModel.Name)}
            FROM users";
        
        using var conn = _dataSource.OpenConnection();
        var result = await conn.QueryAsync<UserDbModel>(sql);
        return result.Select(x => x.ToModel());
    }
    

    public async Task<UserModel> CreateUser(UserModel user)
    {
        var newUser = user.ToDbModel();
        var sql = $@"INSERT INTO users (userId, name) 
                VALUES (@id, @name) RETURNING
                userId as {nameof(UserDbModel.Id)},
                name as {nameof(UserDbModel.Name)}";
        
        using var conn = _dataSource.OpenConnection();
        
        var result = await conn.QueryFirstAsync<UserDbModel>(sql, new {
            Id = Guid.NewGuid(),
            name = newUser.Name
        });
        
        return result.ToModel();
    }

    public async Task<UserModel> UpdateUser(UserModel user)
    {
        var newUser = user.ToDbModel();
        var sql = $@"UPDATE users SET name = @name WHERE userId = @id
                RETURNING
                userId as {nameof(UserDbModel.Id)},
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
        var sql = $@"DELETE FROM users WHERE userId = @id";
        
        using var conn = _dataSource.OpenConnection();
        
        var result = await conn.ExecuteAsync(sql, new { id = id.ToString() }) == 1;

        return result;
    }
}