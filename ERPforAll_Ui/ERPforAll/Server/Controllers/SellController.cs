using ERPforAll.Server.Infrastructure;
using ERPforAll.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERPforAll.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SellController : ControllerBase
    {
        private readonly IDbContextFactory<ErpDBContext> _dbContextFactory;

        public SellController(IDbContextFactory<ErpDBContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        [HttpGet]
        public IEnumerable<Sell> GetAllSells()
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Sells.Include("Customer").Include("Item").ToList();
            }
        }

        [HttpGet("{id}")]
        public Sell? GetById(int id)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Sells.Include("Customer").Include("Item").FirstOrDefault(v => v.Id == id);
            }
        }

        [HttpPost]
        public IActionResult CreateItem([FromBody] Sell sell)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                sell.CustomerId = sell.Customer.Id;
                sell.Customer = null;
                sell.ItemId = sell.Item.Id;
                sell.Item = null;
                sell.Date = DateTime.Now;
                sell.TradeId = Guid.NewGuid();

                context.Sells.Add(sell);
                context.SaveChanges();
            }
            return Ok();
        }

        [HttpPost("{id}")]
        public IActionResult UpdateItem(int id, [FromBody] Sell sell)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var oldSell = context.Sells.Find(sell.Id);

                if (oldSell != null)
                {
                    sell.CustomerId = sell.Customer.Id;
                    sell.Customer = null;
                    oldSell.Item = sell.Item;
                    oldSell.ItemId = sell.Item.Id;
                    oldSell.Amount = sell.Amount;

                    context.Sells.Update(oldSell);
                    context.SaveChanges();
                }
            }
            return Ok();
        }
    }
}
