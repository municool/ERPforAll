using ERPforAll.Server.Infrastructure;
using ERPforAll.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERPforAll.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockController : ControllerBase
    {
        private readonly IDbContextFactory<ErpDBContext> _dbContextFactory;

        public StockController(IDbContextFactory<ErpDBContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        [HttpGet]
        public IEnumerable<Stock> GetAllStocks()
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Stocks.Include("Warehouse").Include("Item").ToList();
            }
        }

        [HttpGet("{id}")]
        public Stock? GetById(int id)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Stocks.Include("Warehouse").Include("Item").FirstOrDefault(v => v.Id == id);
            }
        }

        [HttpPost]
        public IActionResult CreateItem([FromBody] Stock stock)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                stock.WarehouseId = stock.Warehouse.Id;
                stock.Warehouse = null;
                stock.ItemId = stock.Item.Id;
                stock.Item = null;

                context.Stocks.Add(stock);
                context.SaveChanges();
            }
            return Ok();
        }

        [HttpPost("{id}")]
        public IActionResult UpdateItem(int id, [FromBody] Stock stock)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var oldStock = context.Stocks.Find(stock.Id);

                if (oldStock != null)
                {
                    oldStock.Warehouse = stock.Warehouse;
                    oldStock.WarehouseId = stock.Warehouse.Id;
                    oldStock.Item = stock.Item;
                    oldStock.ItemId = stock.Item.Id;
                    oldStock.Amount = stock.Amount;

                    context.Stocks.Update(oldStock);
                    context.SaveChanges();
                }
            }
            return Ok();
        }
    }
}
