using ERPforAll.Server.Infrastructure;
using ERPforAll.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERPforAll.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IDbContextFactory<ErpDBContext> _dbContextFactory;

        public ItemsController(IDbContextFactory<ErpDBContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        [HttpGet]
        public IEnumerable<Item> GetAllItems()
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Items.ToList();
            }
        }

        [HttpGet("{id}")]
        public Item? GetById(int id)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Items.FirstOrDefault(v => v.Id == id);
            }
        }

        [HttpPost]
        public IActionResult CreateItem([FromBody] Item item)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.Items.Add(item);
                context.SaveChanges();
            }
            return Ok();
        }

        [HttpPost("{id}")]
        public IActionResult UpdateItem(int id, [FromBody] Item item)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var oldItem = context.Items.Find(item.Id);

                if (oldItem != null)
                {
                    oldItem.Name = item.Name;

                    context.Items.Update(oldItem);
                    context.SaveChanges();
                }
            }
            return Ok();
        }
    }
}
