namespace PlantCatalog.Models
{
    /* record type (similar to class type)
        1. used for immutable objects 
        2. can use "With"-expressions
        3. can use Value-based equality
    */

    /*
        init makes the property immutable EXCEPT when the object in initialized.
    */

    public record Plant
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public int MinZone { get; init; }
        public int MaxZone { get; init; }
        public DateTimeOffset CreationDate { get; init; } 
    }
}