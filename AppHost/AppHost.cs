var builder = DistributedApplication.CreateBuilder(args);

var userPostgresPassword = builder.AddParameter("PostgresPassword", secret: true);

//DBs
var mongo = builder.AddMongoDB("mongo")
    .WithDataVolume()
    .AddDatabase("EventCatalogDb");

var postgres = builder.AddPostgres("postgres", password: userPostgresPassword)
    .WithDataVolume("postgres-volume")
    .WithPgAdmin();

var venueDb = postgres.AddDatabase("VenueDb");
var userProfileDb = postgres.AddDatabase("UserProfileDb");

var redis = builder.AddRedis("Redis")
    .WithDataVolume();


//Services
var eventCatalog = builder.AddProject<Projects.EventCatalogApi>("EventCatalogService")
    //.WithReplicas(3)
    .WithReference(mongo)
    .WithReference(redis)
    .WaitFor(mongo)
    .WaitFor(redis);

var userProfile = builder.AddProject<Projects.UserProfileServiceAPI>("UserProfileService")
    //.WithReplicas(3)
    .WithReference(redis)
    .WithReference(userProfileDb)
    .WaitFor(postgres)
    .WaitFor(redis);

var venue = builder.AddProject<Projects.VenueService>("VenueService")
    //.WithReplicas(3)
    .WithReference(redis)
    .WithReference(venueDb)
    .WaitFor(postgres)
    .WaitFor(redis);

//Aggregator
var aggregator = builder.AddProject<Projects.Aggregator>("Aggregator")
    .WithReference(redis)
    .WithReference(venue)
    .WithReference(eventCatalog)
    .WithReference(userProfile)
    .WaitFor(venue)
    .WaitFor(userProfile)
    .WaitFor(eventCatalog)
    .WaitFor(redis);

//Api Gateway
var gateway = builder.AddProject<Projects.ApiGateway>("ApiGateway")
    .WithHttpEndpoint(port: 5000, name: "gateway") //htpp issue
    .WithReference(eventCatalog)
    .WithReference(venue)
    .WithReference(userProfile)
    .WithReference(aggregator)
    .WaitFor(eventCatalog)
    .WaitFor(venue)
    .WaitFor(userProfile)
    .WaitFor(aggregator);


builder.Build().Run();