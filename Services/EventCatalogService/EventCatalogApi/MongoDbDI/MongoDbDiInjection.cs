using Domain.Interfaces;
using Infrastructure;
using Infrastructure.Common;
using Infrastructure.Configuration;
using Infrastructure.DbContext;
using Infrastructure.Repositories;
using MongoDB.Driver;

namespace Check.MongoDbDI;


public static class MongoDbDiInjection
{
    public static IServiceCollection AddMongoDb(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        BsonConfiguration.Configure();
        
        // var connectionString = configuration.GetConnectionString("MongoDb");
        var connectionString = configuration.GetConnectionString("EventCatalogDb");
        var dbName = configuration["MongoDb:DatabaseName"];

        serviceCollection.AddSingleton<IMongoClient>(sp =>
        {
            var settings = MongoClientSettings.FromConnectionString(connectionString);
            return new MongoClient(settings);
        });

        serviceCollection.AddScoped<IMongoDatabase>(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(dbName);
        });
        
        serviceCollection.AddScoped<MongoDbContext>();

        // Register Unit of Work
        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();

        // Register Repositories
        serviceCollection.AddScoped<IEventRepository, EventRepository>();

        return serviceCollection;
    }
    
    public static async Task InitializeDatabaseAsync(IServiceProvider serviceProvider)
    {
        var database = serviceProvider.GetRequiredService<IMongoDatabase>();
    }
}