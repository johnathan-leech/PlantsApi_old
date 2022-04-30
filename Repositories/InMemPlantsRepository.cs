using PlantCatalog.Interfaces;
using PlantCatalog.Models;

namespace PlantCatalog.Repositories
{
    

    public class InMemPlantsRepository : IPlantsRepository
    {
        readonly List<Plant> plants = new(){
            new Plant { Id = Guid.NewGuid(), Name = "Potato", MinZone = 4, MaxZone = 9, CreationDate = DateTimeOffset.UtcNow } ,
            new Plant { Id = Guid.NewGuid(), Name = "Garlic", MinZone = 2, MaxZone = 7, CreationDate = DateTimeOffset.UtcNow } ,
            new Plant { Id = Guid.NewGuid(), Name = "Wheat" , MinZone = 5, MaxZone = 8, CreationDate = DateTimeOffset.UtcNow } ,
        };

        public IEnumerable<Plant> GetPlants()
        {
            return plants;
        }

        public Plant GetPlant(Guid id) => plants.Where(plant => plant.Id == id).SingleOrDefault();






    }
}