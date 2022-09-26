using ERPforAll.Server.Infrastructure;
using ERPforAll.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERPforAll.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PurchaseController : ControllerBase
    {
        private readonly IDbContextFactory<ErpDBContext> _dbContextFactory;

        public PurchaseController(IDbContextFactory<ErpDBContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        [HttpGet]
        public IEnumerable<Purchase> GetAllPurchases()
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Purchases.Include("Vendor").Include("Item").ToList();
            }
        }

        [HttpGet("{id}")]
        public Purchase? GetById(int id)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Purchases.Include("Vendor").Include("Item").FirstOrDefault(v => v.Id == id);
            }
        }

        [HttpPost]
        public IActionResult CreateItem([FromBody] Purchase purchase)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                purchase.VendorId = purchase.Vendor.Id;
                purchase.Vendor = null;
                purchase.ItemId = purchase.Item.Id;
                purchase.Item = null;
                purchase.Date = DateTime.Now;
                purchase.TradeId = Guid.NewGuid();

                context.Purchases.Add(purchase);
                context.SaveChanges();
            }
            return Ok();
        }

        [HttpPost("{id}")]
        public IActionResult UpdateItem(int id, [FromBody] Purchase purchase)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var oldPurchase = context.Purchases.Find(purchase.Id);

                if (oldPurchase != null)
                {
                    purchase.VendorId = purchase.Vendor.Id;
                    purchase.Vendor = null;
                    oldPurchase.Item = purchase.Item;
                    oldPurchase.ItemId = purchase.Item.Id;
                    oldPurchase.Amount = purchase.Amount;

                    context.Purchases.Update(oldPurchase);
                    context.SaveChanges();
                }
            }
            return Ok();
        }
    }
}
