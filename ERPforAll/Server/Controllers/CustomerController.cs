using ERPforAll.Server.Infrastructure;
using ERPforAll.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERPforAll.Server.Controllers
{
    public class CustomerController : ControllerBase
    {
        private readonly IDbContextFactory<ErpDBContext> _dbContextFactory;

        public CustomerController(IDbContextFactory<ErpDBContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public IEnumerable<Associate> GetAllVendors()
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Customers.ToList();
            }
        }

        public Associate? GetById(int id)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Customers.FirstOrDefault(v => v.Id == id);
            }
        }
    }
}
