using Domain.Exceptions;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities;

public class EventCategory : BaseEntity
{
    [BsonElement("name")]
    public string Name { get; private set; }

    [BsonElement("description")]
    public string Description { get; private set; }

    // Private constructor for BSON
    private EventCategory() : base() {}

    public EventCategory(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Category name cannot be empty.");

        Name = name;
        Description = description;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    // Public method to change state, ensuring validation
    public void UpdateDetails(string newName, string newDescription)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new DomainException("Category name cannot be empty.");
            
        Name = newName;
        Description = newDescription;
        UpdatedAt = DateTime.UtcNow;
    }
}