using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Infastructure.Models;

public class UserDbModel
{
    [BsonId]
    public ObjectId Id { get; set; }
    public string Name { get; set; }
}