using PlantCatalog.Models;

namespace PlantCatalog.Interfaces
{
    public interface IPlantsRepository
    {
        Plant GetPlant(Guid id);
        IEnumerable<Plant> GetPlants();
        void Create(Plant plant);
        void Update(Plant plant);
        void Delete(Guid id);
    }

}