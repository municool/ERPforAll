using ERPforAll.Server.Infrastructure;
using ERPforAll.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERPforAll.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WarehouseController : ControllerBase
    {
        private readonly IDbContextFactory<ErpDBContext> _dbContextFactory;

        public WarehouseController(IDbContextFactory<ErpDBContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        [HttpGet]
        public IEnumerable<Warehouse> GetAllWarehouses()
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Warehouses.ToList();
            }
        }

        [HttpGet("{id}")]
        public Warehouse? GetById(int id)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Warehouses.FirstOrDefault(v => v.Id == id);
            }
        }

        [HttpPost]
        public IActionResult CreateItem([FromBody] Warehouse warehouse)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.Warehouses.Add(warehouse);
                context.SaveChanges();
            }
            return Ok();
        }

        [HttpPost("{id}")]
        public IActionResult UpdateItem(int id, [FromBody] Warehouse warehouse)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var oldWarehouse = context.Warehouses.Find(warehouse.Id);

                if (oldWarehouse != null)
                {
                    oldWarehouse.Name = warehouse.Name;

                    context.Warehouses.Update(oldWarehouse);
                    context.SaveChanges();
                }
            }
            return Ok();
        }
    }
}
