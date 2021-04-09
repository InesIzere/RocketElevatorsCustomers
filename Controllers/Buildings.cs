using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rocket_Elevators_Rest_API.Models;
using Microsoft.EntityFrameworkCore;

using Pomelo.EntityFrameworkCore.MySql;

namespace Rocket_Elevators_Rest_API.Models.Controllers
{
  [Route("api/[controller]")]
    [ApiController]
    public class BuildingsController : ControllerBase
    {
        private readonly shimwe_mysqlContext _context;

        public BuildingsController(shimwe_mysqlContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Buildings>>> GetBuildings()
        {
            return await _context.Buildings.ToListAsync();
        }

    // GET: api/Building/{id}
      [HttpGet("{id}")]
        public async Task<ActionResult<Buildings>> GetBuilding(long id)
        {
            var building = await _context.Buildings.FindAsync(id);

            if (building == null)
            {
                return NotFound();
            }

            return building;
        }
     //Get only batteries that belong to a particular building

    //   [HttpGet("{id}/buil")]
    //     public async Task<ActionResult<Buildings>> GetBuildings(long id)

    //     {
    //         var building = await _context.Buildings.Where(b => b.CustomerId == id).ToListAsync();
    //         if (building == null)
    //         {
    //             return NotFound();
    //         }

    //          return  new OkObjectResult("success");
    //     }


     [HttpGet("customer/{id}")]
        public async Task<ActionResult<IEnumerable<Buildings>>> Getbuildings(long id)
        {
            var building = await _context.Buildings.Where(b => b.CustomerId == id).ToListAsync();
            if (building == null)
            {
                return NotFound();
            }

            return building;

        }

      // GET: api/buildings
        // Retrieving a list of Buildings requiring intervention 
       [HttpGet("Intervention")]
        public ActionResult<List<Buildings>> GetToFixBuildings()
        {
            IQueryable<Buildings> ToFixBuildingsList = from bat in _context.Buildings
            join Batteries in _context.Batteries on bat.Id equals Batteries.BuildingId 
            join Columns in _context.Columns on Batteries.Id equals Columns.BatteryId
            join Elevators in _context.Elevators on Columns.Id equals Elevators.ColumnId
            where (Batteries.Status == "Intervention") || (Columns.Status == "Intervention") || (Elevators.Status == "Intervention")
            select bat;
            return ToFixBuildingsList.Distinct().ToList();
        }
       
        // DELETE: Batteries
        [HttpDelete("{id}")]
        public async Task<ActionResult<Buildings>> DeleteBuilding(int id)
        {
            var building = await _context.Buildings.FindAsync(id);
            if (building == null)
            {
                return NotFound();
            }

            _context.Buildings.Remove(building);
            await _context.SaveChangesAsync();

            return building;
        }

        private bool BuildingExists(long id)
        {
            return _context.Buildings.Any(e => e.Id == id);
        }

      
    }

    
}