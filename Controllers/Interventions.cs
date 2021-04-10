using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rocket_Elevators_Rest_API.Models;
using Microsoft.EntityFrameworkCore;



namespace Rocket_Elevators_Rest_API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]

  public class InterventionsController : ControllerBase
  {
    private readonly shimwe_mysqlContext _context;

    public InterventionsController(shimwe_mysqlContext context)
    {
      _context = context;
    }
    // GET:  the list the interventions that are in the database.
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Interventions>>> GetInterventions()
    {
      return await _context.Interventions.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Interventions>> GetIntervention(long id)
    {
      var intervention = await _context.Interventions.FindAsync(id);

      if (intervention == null)
      {
        return NotFound();
      }

      return intervention;
    }


    // GET: Returns all fields of all Service Request records that do not have a start date and are in "Pending" status.
    [HttpGet("Pending")]
    public async Task<ActionResult<IEnumerable<Interventions>>> GetpendingIntervention()
    {
      // Create a list of interventions where the status is "Pending" and the start_date is null
      var Request_records = from stat in _context.Interventions
                            where stat.StartDate == null &&
                            stat.Status == "Pending"
                            select stat;

      // Return
      return await Request_records.ToListAsync();
    }

    // PUT: Change the status of the intervention request to "InProgress" and add a start date and time (Timestamp).
    [HttpPut("StartDate/{id}")]
    public async Task<ActionResult<Interventions>> InterventionStarted(long id)
    {

      var intervention = await _context.Interventions.FindAsync(id);

      // see if the intervention exists
      if (id != intervention.Id)
      {
        return BadRequest();
      }

      //changing the status
      intervention.StartDate = DateTime.Now;
      intervention.Status = "InProgress";

      //save
      await _context.SaveChangesAsync();

      // get a return 
      return intervention;
    }

    //PUT: Change the status of the request for action to "Completed" and add an end date and time (Timestamp).
    [HttpPut("EndDate/{id}")]

    public async Task<ActionResult<Interventions>> InterventionFinish(long id)
    {

      var intervention = await _context.Interventions.FindAsync(id);

      // Check if the intervention exists
      if (id != intervention.Id)
      {
        return BadRequest();
      }

      // Change the status of the request 
      intervention.EndDate = DateTime.Now;
      intervention.Status = "Completed";

      //save
      await _context.SaveChangesAsync();

      //get return
      return intervention;
    }


    // [HttpPost]
    // public async Task<ActionResult<Interventions>> PostIntervention(Interventions intervention)
    // {
    //     _context.Interventions.Add(intervention);
    //     await _context.SaveChangesAsync();

    //     return CreatedAtAction(nameof(GetIntervention), new { id = intervention.Id }, intervention);
    // }
    [HttpPost]
    public async Task<IActionResult> PostIntervention([FromBody] Interventions student)
    {
      if (student == null)
      {
        return BadRequest();
      };

      using (var inter = new shimwe_mysqlContext())
      {
        inter.Interventions.Add(new Interventions()
        {  
          AuthorId = student.ElevatorId,
          BuildingId = student.BuildingId,
          BatteryId = student.BatteryId,
          ColumnId = student.ColumnId,
          ElevatorId = student.ElevatorId,
          created_at = DateTime.Now,
          updated_at = DateTime.Now,
          CustomerId = student.CustomerId

        });
        inter.SaveChanges();


      }

      return Ok();
    }



  }

}
