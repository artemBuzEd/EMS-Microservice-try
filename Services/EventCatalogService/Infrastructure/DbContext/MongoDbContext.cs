using Domain.Entities;
using MongoDB.Driver;

namespace Infrastructure.DbContext;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;
    public IMongoCollection<Event> Events => _database.GetCollection<Event>("events");
    public IMongoCollection<EventCategory> EventCategories => _database.GetCollection<EventCategory>("event_categories");

    public MongoDbContext(IMongoDatabase database)
    {
        if(database is null)
            throw new ArgumentNullException(nameof(database));
        _database = database;
    }
    
    
}