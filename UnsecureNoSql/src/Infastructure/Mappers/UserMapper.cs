using Infastructure.Models;

namespace Infastructure.Mappers;

public static class UserMapper
{
    public static User ToDomain(this UserDbModel user)
    {
        return new User
        {
            Id = new UserId(user.Id),
            Name = user.Name,
        };
    }
    public static UserDbModel ToDbModel(this User user)
    {
        return new UserDbModel
        {
            Id = user.Id.Value,
            Name = user.Name,
        };
    }
}