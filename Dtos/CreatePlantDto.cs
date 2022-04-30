using System.ComponentModel.DataAnnotations;

namespace PlantCatalog.Dtos
{
    public record CreatePlantDto
    {
        [Required]
        public string Name { get; init; }
        [Required]
        [Range(1, 12)]
        public int MinZone { get; init; }
        [Required]
        [Range(1, 12)]
        public int MaxZone { get; init; }
    }


}