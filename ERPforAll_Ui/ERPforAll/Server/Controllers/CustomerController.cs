using ERPforAll.Client.Pages;
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
        public IActionResult CreateItem([FromBody] Customer customer)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.Customers.Add(customer);
                context.SaveChanges();
            }
            return Ok();
        }

        [HttpPost("{id}")]
        public IActionResult UpdateItem(int id, [FromBody] Customer customer)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var oldCustomer = context.Customers.Find(customer.Id);

                if (oldCustomer != null)
                {
                    oldCustomer.Name = customer.Name;

                    context.Customers.Update(oldCustomer);
                    context.SaveChanges();
                }
            }
            return Ok();
        }
    }
}
