using Domain;
using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;

namespace Infrastructure.Configuration;

public class BsonConfiguration
{
    private static bool _isConfigured = false;

    public static void Configure()
    {
        if(_isConfigured) return;
        
        RegisterConventions();
        
        RegisterCustomSerializers();
        
        
        var pack = new ConventionPack
        {
            new CamelCaseElementNameConvention(),
            new IgnoreExtraElementsConvention(true),
            new EnumRepresentationConvention(MongoDB.Bson.BsonType.String)
        };
        ConventionRegistry.Register("defaultConventions", pack, t => true); 
        
        RegisterEntityMaps();
        
        _isConfigured = true;
    }
    
    private static void RegisterCustomSerializers()
    {
        BsonSerializer.RegisterSerializer(typeof(EventDateRange), new EventDateRangeSerializer());
        BsonSerializer.RegisterSerializer(typeof(Location), new LocationSerializer());
    }
    
    private static void RegisterConventions()
    {
        var pack = new ConventionPack
        {
            new CamelCaseElementNameConvention(),
            new IgnoreExtraElementsConvention(true),
            new EnumRepresentationConvention(BsonType.String),
            new IgnoreIfNullConvention(true)
        };
        ConventionRegistry.Register("DefaultConventions", pack, t => true);
    }
    
    private static void RegisterEntityMaps()
    {
        if (!BsonClassMap.IsClassMapRegistered(typeof(BaseEntity)))
        {
            BsonClassMap.RegisterClassMap<BaseEntity>(cm =>
            {
                cm.AutoMap();
                cm.SetIsRootClass(true);
                cm.MapIdMember(e => e.Id)
                    .SetIdGenerator(MongoDB.Bson.Serialization.IdGenerators.StringObjectIdGenerator.Instance);
                cm.MapProperty(e => e.CreatedAt).SetElementName("createdAt");
                cm.MapProperty(e => e.UpdatedAt).SetElementName("updatedAt");
                cm.SetIgnoreExtraElements(true);
            });
        }
        
        if (!BsonClassMap.IsClassMapRegistered(typeof(Event)))
        {
            BsonClassMap.RegisterClassMap<Event>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
                
                cm.MapMember(e => e.Title).SetElementName("title");
                cm.MapMember(e => e.Description).SetElementName("description");
                cm.MapMember(e => e.DateRange).SetElementName("dateRange");
                cm.MapMember(e => e.Location).SetElementName("location");
                cm.MapMember(e => e.Category).SetElementName("category");
                cm.MapMember(e => e.OrganizerId).SetElementName("organizerId");
                cm.MapMember(e => e.VenueId).SetElementName("venueId");
                cm.MapMember(e => e.Capacity).SetElementName("capacity");
                
                cm.MapCreator(e => new Event(
                    e.Title, 
                    e.Description, 
                    e.DateRange, 
                    e.Location, 
                    e.Category, 
                    e.OrganizerId, 
                    e.VenueId, 
                    e.Capacity));
            });
        }
        
        if (!BsonClassMap.IsClassMapRegistered(typeof(EventCategory)))
        {
            BsonClassMap.RegisterClassMap<EventCategory>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
                cm.MapMember(c => c.Name).SetElementName("name");
                cm.MapMember(c => c.Description).SetElementName("description");
                cm.MapCreator(c => new EventCategory(c.Name, c.Description));
            });
        }
    }
}