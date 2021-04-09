using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Rocket_Elevators_Rest_API.Models;
using System.Collections.Generic;


namespace Rocket_Elevators_Rest_API.Models.Controllers
{
  [Route("api/[controller]")]
    [ApiController]
    public class LeadsController : ControllerBase
    {
        //Create context attribute
        private readonly shimwe_mysqlContext _context;
        //constructor 
        public LeadsController(shimwe_mysqlContext context)
        {
            _context = context;
        }

        // Get list of leads                                    
        // GET: api/leads           
        [HttpGet]
        public IEnumerable<Leads> GetLeads()
        {
          //Prepare the query 
            IQueryable<Leads> Leads =
            from l in _context.Leads
            select l;
            return Leads.ToList();

        }
        //Retrieving a list of Leads created in the last 30 days who have not yet become customers.
        [HttpGet("30days")]
         public IEnumerable<Leads> GetLead()
         {
            //Set the date 
            DateTime today = DateTime.Now;
            DateTime answer = today.AddDays(-30);
            //Prepare the query 
            IQueryable<Leads> day30snotcustomers =
            from l in _context.Leads
            where l.contact_request_date  >= answer
            select l;
            return day30snotcustomers.ToList();
         }               
    }
}