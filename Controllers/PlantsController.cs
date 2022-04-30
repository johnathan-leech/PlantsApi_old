using Microsoft.AspNetCore.Mvc;
using PlantCatalog.Dtos;
using PlantCatalog.Interfaces;
using PlantCatalog.Models;

namespace PlantCatalog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlantsController : ControllerBase
    {
        readonly IPlantsRepository repository;

        public PlantsController(IPlantsRepository repository){
            this.repository = repository;
        }

        // GET /Plants
        [HttpGet]
        public IEnumerable<PlantDto> GetPlants(){
            var plants = repository.GetPlants().Select(plant => plant.AsDto());
            return plants;
        }

        // GET /Plants/id
        [HttpGet("{id}")]
        public ActionResult<PlantDto> GetPlant(Guid id){
            var plant = repository.GetPlant(id);
            if (plant is null) { return NotFound(); }
            return plant.AsDto();
        }

        // POST /Plants
        [HttpPost]
        public ActionResult<PlantDto> Create(CreatePlantDto dto)
        {
            Plant plant = new() { 
                Id           = Guid.NewGuid(), 
                Name         = dto.Name, 
                MinZone      = dto.MinZone, 
                MaxZone      = dto.MaxZone, 
                CreationDate = DateTimeOffset.UtcNow 
            };
            repository.Create(plant);
            return CreatedAtAction(nameof(GetPlant), new { id = plant.Id }, plant.AsDto()); // gets the plant information that was just created.
        }

        // PUT /Plants/{id}
        [HttpPut("{id}")]
        public ActionResult Update(Guid id, UpdatePlantDto dto)
        {
            var existing = repository.GetPlant(id);
            if (existing is null) { return NotFound(); }
            
            Plant updated = existing with 
            {
                Name    = dto.Name ,
                MinZone = dto.MinZone ,
                MaxZone = dto.MaxZone
            };
            repository.Update(updated);
            return NoContent();
        }

        // DELETE /Plants/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var existing = repository.GetPlant(id);
            if (existing is null) { return NotFound(); }
            repository.Delete(id);
            return NoContent();
        }
    }
}