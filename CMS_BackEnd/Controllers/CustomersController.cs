using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using CMS_BackEnd.Models;
using System;

namespace CMS_BackEnd.Controllers
{
    [RoutePrefix("api/customers")]
    public class CustomersController : ApiController
    {
        private readonly CustomerContext _context;

        public CustomersController() : this(new CustomerContext()) { }

        public CustomersController(CustomerContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET: api/customers
        [HttpGet]
        [Route("")]
        public IQueryable<Customer> GetCustomers()
        {
            return _context.Customers;
        }

        // GET: api/customers/{id}
        [HttpGet]
        [Route("{id:int}", Name = "GetCustomer")]
        [ResponseType(typeof(Customer))]
        public async Task<IHttpActionResult> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        // PUT: api/customers/{id}
        [HttpPut]
        [Route("{id:int}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != customer.Id)
                return BadRequest("Customer ID mismatch");

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                    return NotFound();

                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/customers
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(Customer))]
        public async Task<IHttpActionResult> PostCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Customers.Add(customer);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return CreatedAtRoute("GetCustomer", new { id = customer.Id }, customer);


        }

        // DELETE: api/customers/{id}
        [HttpDelete]
        [Route("{id:int}")]
        [ResponseType(typeof(Customer))]
        public async Task<IHttpActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                return NotFound();

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return Ok(customer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}