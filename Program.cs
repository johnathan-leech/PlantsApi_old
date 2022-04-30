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

// Add services to the container.
builder.Services.AddSingleton<IMongoClient>(serviceProvider => 
{
    var config = builder.Configuration.GetSection(nameof(MongoDbConfig)).Get<MongoDbConfig>();
    return new MongoClient(config.ConnectionString);
});

builder.Services.AddSingleton<IPlantsRepository, MongoDbPlantsRepository>();
builder.Services.AddControllers(options => 
{
    options.SuppressAsyncSuffixInActionNames = false;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
