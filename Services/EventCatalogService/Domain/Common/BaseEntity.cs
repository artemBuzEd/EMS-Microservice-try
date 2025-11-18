using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain;

public abstract class BaseEntity 
{   
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; protected set; }
    
    
    public DateTime CreatedAt { get; protected set; }
    public DateTime UpdatedAt { get; protected set; }

    protected BaseEntity()
    {
        
    }
}