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
        public async Task<IEnumerable<PlantDto>> GetPlantsAsync(){
            var plants = (await repository.GetPlantsAsync()).Select(plant => plant.AsDto());
            return plants;
        }

        // GET /Plants/id
        [HttpGet("{id}")]
        public async Task<ActionResult<PlantDto>> GetPlantAsync(Guid id){
            var plant = await repository.GetPlantAsync(id);
            if (plant is null) { return NotFound(); }
            return plant.AsDto();
        }

        // POST /Plants
        [HttpPost]
        public async Task<ActionResult<PlantDto>> CreateAsync(CreatePlantDto dto)
        {
            Plant plant = new() { 
                Id           = Guid.NewGuid(), 
                Name         = dto.Name, 
                MinZone      = dto.MinZone, 
                MaxZone      = dto.MaxZone, 
                CreationDate = DateTimeOffset.UtcNow 
            };
            await repository.CreateAsync(plant);
            return CreatedAtAction(nameof(GetPlantAsync), new { id = plant.Id }, plant.AsDto()); // gets the plant information that was just created.
        }

        // PUT /Plants/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(Guid id, UpdatePlantDto dto)
        {
            var existing = await repository.GetPlantAsync(id);
            if (existing is null) { return NotFound(); }
            
            Plant updated = existing with 
            {
                Name    = dto.Name ,
                MinZone = dto.MinZone ,
                MaxZone = dto.MaxZone
            };
            await repository.UpdateAsync(updated);
            return NoContent();
        }

        // DELETE /Plants/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var existing = await repository.GetPlantAsync(id);
            if (existing is null) { return NotFound(); }
            await repository.DeleteAsync(id);
            return NoContent();
        }
    }
}