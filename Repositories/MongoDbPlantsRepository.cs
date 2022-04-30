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
            throw new NotImplementedException();
        }

        public Plant GetPlant(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Plant> GetPlants()
        {
            throw new NotImplementedException();
        }

        public void Update(Plant plant)
        {
            throw new NotImplementedException();
        }
    }



}