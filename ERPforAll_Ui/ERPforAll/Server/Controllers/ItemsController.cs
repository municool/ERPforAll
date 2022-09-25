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
        public IEnumerable<Item> Get()
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Items.Include("Stocks").ToList();
            }
        }

        public Item? GetById(int id)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Items.FirstOrDefault(v => v.Id == id);
            }
        }

        public void CreateItem(Item newItem)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.Items.Add(newItem);
                context.SaveChanges();
            }
        }

        public void UpdateItem(Item item)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var oldItem = context.Items.FirstOrDefault(i => i.Id == item.Id);

                if(oldItem != null)
                {
                    context.Items.Update(item);
                }
            }
        }
    }
}
