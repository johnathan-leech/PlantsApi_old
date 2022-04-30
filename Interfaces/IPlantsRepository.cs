using PlantCatalog.Models;

namespace PlantCatalog.Interfaces
{
    public interface IPlantsRepository
    {
        Plant GetPlant(Guid id);
        IEnumerable<Plant> GetPlants();
        void Create(Plant plant);
    }

}