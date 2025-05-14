using MongoDB.Bson;

namespace Infastructure;

public readonly record struct UserId(ObjectId Value);