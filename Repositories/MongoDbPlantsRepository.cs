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

        public void Create(Plant plant)
        {
            plantsCollection.InsertOne(plant);
        }

        public void Delete(Guid id)
        {
            var filter = filterBuilder.Eq(plant => plant.Id, id);
            plantsCollection.DeleteOne(filter);
        }

        public Plant GetPlant(Guid id)
        {
            var filter = filterBuilder.Eq(plant => plant.Id, id);
            return plantsCollection.Find(filter).SingleOrDefault();
        }

        public IEnumerable<Plant> GetPlants()
        {
            return plantsCollection.Find(new BsonDocument()).ToList();
        }

        public void Update(Plant plant)
        {
            var filter = filterBuilder.Eq(existing => existing.Id, plant.Id);
            plantsCollection.ReplaceOne(filter, plant);
        }
    }



}