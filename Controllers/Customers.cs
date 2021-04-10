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
    public class CustomersController : ControllerBase
    {
        private readonly shimwe_mysqlContext _context;

        public CustomersController(shimwe_mysqlContext context)
        {
            _context = context;
        }

        //Action that gives the list of all customers
        // GET: api/customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customers>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Customers>> GetCustomers(long id)
        {
            var Customer = await _context.Customers.FindAsync(id);

            if (Customer == null)
            {
                return NotFound();
            }

            return Customer;
        }


    //    //GET Customers by his email
    //     [HttpGet("{email}")]
    //     public IEnumerable<Customers> GetCustomersAuth([FromRoute] string email)
    //     {
    //         IEnumerable<Customers> customer = _context.Customers.Where(c => c.CompanyContactEmail.Equals(email))
    //         return customer;
    //     }
     // GET: api/Customers/email
        // [HttpGet("{email}")]
        [HttpGet("email/{email}")]
        public async Task<ActionResult<Customers>> GetCustomerEmail(string email)
        {

            IEnumerable<Customers> customersAll = await _context.Customers.ToListAsync();

            foreach (Customers customer in customersAll)
            {
                if (customer.CompanyContactEmail == email)
                {
                    return customer;
                }
            }
            return NotFound();
        }



        // [HttpPost]
        // public async Task<ActionResult<Customers>> PostCustomers(Customers Customers)
        // {
        //     _context.Customers.Add(Customers);
        //     await _context.SaveChangesAsync();

        //     return CreatedAtAction("GetCustomers", new { id = Customers.Id }, Customers);
        // }
        [HttpPost("update")]
        public async Task<IActionResult> PutmodifyCustomer(Customers cust)
        {

            if (cust == null)
            {
                return BadRequest();
            }
            var cus = await _context.Customers.FindAsync(cust.Id);

            cus.FullNameCompanyContact = cust.FullNameCompanyContact;
            cus.CompanyContactPhone = cust.CompanyContactPhone;
            cus.CompanyContactEmail = cust.CompanyContactEmail;
            cus.TechnicalAuthorityEmail = cust.TechnicalAuthorityEmail;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!customersExist(cus.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool customersExist(long id)
        {
            return _context.Customers.Any(e => e.Id == id);
           
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Customers>> DeleteCustomers(long id, string Temail)
        {
            var Customers = await _context.Customers.FindAsync(id);
            if (Customers == null)
            {
                return NotFound();
            }

           var CustomerEmail = await _context.Customers.FindAsync(Temail);
            if (CustomerEmail == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(Customers);
            await _context.SaveChangesAsync();

            return Customers;
        }

        

         private bool CustomersemailExists(string Temail)
        {
             return _context.Customers.Any( e => e.CompanyContactEmail == Temail);
           
        }

     


        
    
    }
}