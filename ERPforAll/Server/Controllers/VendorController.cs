using ERPforAll.Server.Infrastructure;
using ERPforAll.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERPforAll.Server.Controllers
{
    public class VendorController : ControllerBase
    {
        private readonly IDbContextFactory<ErpDBContext> _dbContextFactory;
        public VendorController(IDbContextFactory<ErpDBContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public IEnumerable<Associate> GetAllVendors()
        {
            using(var context = _dbContextFactory.CreateDbContext())
            {
                return context.Vendors.ToList();
            }
        }

        public Associate? GetById(int id)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Vendors.FirstOrDefault(v => v.Id == id);
            }
        }
    }
}
