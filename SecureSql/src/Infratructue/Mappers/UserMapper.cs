using Infratructue.Models;

namespace Infratructue.Mappers;

public static class UserMapper
{
    public static UserModel ToModel(this UserDbModel dbModel)
    {
        return new UserModel
        {
            Id = new Guid(dbModel.Id),
            Name = dbModel.Name
        };
    }


    public static UserDbModel ToDbModel(this UserModel model)
    {
        return new UserDbModel
        {
            Id = model.Id.ToString(),
            Name = model.Name
        };
    }
}