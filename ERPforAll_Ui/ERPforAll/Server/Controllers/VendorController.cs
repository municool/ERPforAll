using ERPforAll.Server.Infrastructure;
using ERPforAll.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERPforAll.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendorController : ControllerBase
    {
        private readonly IDbContextFactory<ErpDBContext> _dbContextFactory;

        public VendorController(IDbContextFactory<ErpDBContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        [HttpGet]
        public IEnumerable<Vendor> GetAllVendors()
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Vendors.ToList();
            }
        }

        [HttpGet("{id}")]
        public Vendor? GetById(int id)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Vendors.FirstOrDefault(v => v.Id == id);
            }
        }

        [HttpPost]
        public IActionResult CreateItem([FromBody] Vendor vendor)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.Vendors.Add(vendor);
                context.SaveChanges();
            }
            return Ok();
        }

        [HttpPost("{id}")]
        public IActionResult UpdateItem(int id, [FromBody] Vendor vendor)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var oldVendor = context.Vendors.Find(vendor.Id);

                if (oldVendor != null)
                {
                    oldVendor.Name = vendor.Name;

                    context.Vendors.Update(oldVendor);
                    context.SaveChanges();
                }
            }
            return Ok();
        }
    }
}
