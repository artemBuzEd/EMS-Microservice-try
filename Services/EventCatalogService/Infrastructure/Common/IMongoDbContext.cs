using Domain.Entities;
using MongoDB.Driver;

namespace Infrastructure.Common;

public interface IMongoDbContext
{
    IMongoCollection<Event> Events { get; }
    IMongoCollection<EventCategory> EventCategories { get; }
    
}