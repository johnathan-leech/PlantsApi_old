using System.Net.Mime;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using PlantCatalog.Config;
using PlantCatalog.Interfaces;
using PlantCatalog.Repositories;

var builder = WebApplication.CreateBuilder(args);

// when Guid and DateTimeOffset datatyped objects are stored in mongoDb, do so as String datatype.
BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));
var mongoDbConfig = builder.Configuration.GetSection(nameof(MongoDbConfig)).Get<MongoDbConfig>();

// Add services to the container.
builder.Services.AddSingleton<IMongoClient>(serviceProvider => 
{
    return new MongoClient(mongoDbConfig.ConnectionString);
});

builder.Services.AddSingleton<IPlantsRepository, MongoDbPlantsRepository>();
builder.Services.AddControllers(options => 
{
    options.SuppressAsyncSuffixInActionNames = false;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks()
    .AddMongoDb(
        mongoDbConfig.ConnectionString, 
        name: "mongodb", 
        timeout: TimeSpan.FromSeconds(5),
        tags: new[] { "ready" }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// check the health status of the service (is it running)
app.MapHealthChecks("/health/live", new HealthCheckOptions{ Predicate = (_) => false });
// check the health status of the database
app.MapHealthChecks("/health/ready", new HealthCheckOptions{ 
    Predicate = (check) => check.Tags.Contains("ready"),
    ResponseWriter = async(context, report) => {
        var result = JsonSerializer.Serialize(
            new{
                status = report.Status.ToString(),
                checks = report.Entries.Select(entry => new{
                    name = entry.Key,
                    status = entry.Value.Status.ToString(),
                    duration = entry.Value.Duration.ToString(),
                    exception = entry.Value.Exception != null ? entry.Value.Exception.Message : "none",
                    description = entry.Value.Description != null ? entry.Value.Description : "none"
                })
            }
        );
        context.Response.ContentType = MediaTypeNames.Application.Json;
        await context.Response.WriteAsync(result);
    }
});

app.Run();
