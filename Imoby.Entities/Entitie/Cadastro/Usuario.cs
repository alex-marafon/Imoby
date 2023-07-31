using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Imoby.Entities.Entitie.Models;
public class Usuario
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
}
