using ERPforAll.Server.Infrastructure;
using ERPforAll.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERPforAll.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IDbContextFactory<ErpDBContext> _dbContextFactory;

        public CustomerController(IDbContextFactory<ErpDBContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        [HttpGet]
        public IEnumerable<Customer> GetAllCustomers()
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Customers.ToList();
            }
        }

        [HttpGet("{id}")]
        public Customer? GetById(int id)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Customers.FirstOrDefault(v => v.Id == id);
            }
        }

        [HttpPost]
        public IActionResult CreateItem([FromBody]Customer newCustomer)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.Customers.Add(newCustomer);
                context.SaveChanges();
            }
            return Ok();
        }

        [HttpPost("{id}")]
        public IActionResult UpdateItem(int id, [FromBody]Customer customer)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var oldItem = context.Customers.FirstOrDefault(i => i.Id == id);

                if (oldItem != null)
                {
                    context.Customers.Update(customer);
                    context.SaveChanges();
                }
            }
            return Ok();
        }
    }
}
