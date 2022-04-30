namespace PlantCatalog.Dtos
{
    public record CreatePlantDto
    {
        public string Name { get; init; }
        public int MinZone { get; init; }
        public int MaxZone { get; init; }
    }


}