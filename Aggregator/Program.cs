using Aggregator.Services;
using EventCatalogApi.Protos;
using ServiceDefaults;
using Grpc;
using Grpc.Net.Client;
using UserProfileApi.Protos;

AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.AddServiceDefaults();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(sp =>
{
    var channel = GrpcChannel.ForAddress("https://localhost:7159", new GrpcChannelOptions
    {
        HttpHandler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        }
    });
    return new EventCatalog.EventCatalogClient(channel);
});

builder.Services.AddSingleton(sp =>
{
    var channel = GrpcChannel.ForAddress("https://localhost:7146", new GrpcChannelOptions
    {
        HttpHandler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        }
    });
    return new UserProfile.UserProfileClient(channel);
});

builder.Services.AddScoped<AggregatorGrpcService>();

builder.Services.AddHttpClient("EventCatalogService", client =>
{
    client.BaseAddress = new Uri("http://EventCatalogService");
});

builder.Services.AddHttpClient("UserProfileService", client =>
{
    client.BaseAddress = new Uri("http://UserProfileService");
});

builder.Services.AddHttpClient("VenueService", client =>
{
    client.BaseAddress = new Uri("http://VenueService");
});

var app = builder.Build();

app.MapDefaultEndpoints();
app.UseCorrelationId();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();