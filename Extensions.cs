using PlantCatalog.Dtos;
using PlantCatalog.Models;

namespace PlantCatalog
{
    internal static class Extensions
    {
        public static PlantDto AsDto(this Plant plant)
        {
            return new PlantDto {
                Id           = plant.Id ,
                Name         = plant.Name ,
                MinZone      = plant.MinZone ,
                MaxZone      = plant.MaxZone ,
                CreationDate = plant.CreationDate
            };
        }

    }
}