using PlantCatalog.Models;

namespace PlantCatalog.Interfaces
{
    public interface IPlantsRepository
    {
        Task<Plant> GetPlantAsync(Guid id);
        Task<IEnumerable<Plant>> GetPlantsAsync();
        Task CreateAsync(Plant plant);
        Task UpdateAsync(Plant plant);
        Task DeleteAsync(Guid id);
    }
}