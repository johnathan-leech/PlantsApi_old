using Microsoft.AspNetCore.Mvc;
using PlantCatalog.Interfaces;
using PlantCatalog.Models;
using PlantCatalog.Repositories;

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
        public IEnumerable<Plant> GetPlants(){
            var plants = repository.GetPlants();
            return plants;
        }

        // GET /Plants/id
        [HttpGet("{id}")]
        public ActionResult<Plant> GetPlant(Guid id){
            var plant = repository.GetPlant(id);

            if (plant is null){
                return NotFound();
            }
            return plant;
        }
        
    }
}