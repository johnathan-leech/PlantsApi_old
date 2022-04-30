using MongoDB.Bson;
using MongoDB.Driver;
using PlantCatalog.Interfaces;
using PlantCatalog.Models;

namespace PlantCatalog.Repositories
{
    public class MongoDbPlantsRepository : IPlantsRepository
    {
        const string databaseName = "plantCatalog";
        const string collectionName = "plants";
        readonly IMongoCollection<Plant> plantsCollection;
        readonly FilterDefinitionBuilder<Plant> filterBuilder = Builders<Plant>.Filter;

        public MongoDbPlantsRepository(IMongoClient client)
        {
            IMongoDatabase database = client.GetDatabase(databaseName);
            plantsCollection = database.GetCollection<Plant>(collectionName);
        }

        public async Task CreateAsync(Plant plant)
        {
            await plantsCollection.InsertOneAsync(plant);
        }

        public async Task DeleteAsync(Guid id)
        {
            var filter = filterBuilder.Eq(plant => plant.Id, id);
            await plantsCollection.DeleteOneAsync(filter);
        }

        public async Task<Plant> GetPlantAsync(Guid id)
        {
            var filter = filterBuilder.Eq(plant => plant.Id, id);
            return await plantsCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Plant>> GetPlantsAsync()
        {
            return await plantsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateAsync(Plant plant)
        {
            var filter = filterBuilder.Eq(existing => existing.Id, plant.Id);
            await plantsCollection.ReplaceOneAsync(filter, plant);
        }
    }
}