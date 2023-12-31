using DataModel;
using Microsoft.EntityFrameworkCore;
using RazorPages.Data;
using RazorPages.IRepositories;

namespace RazorPages.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly ApplicationDbContext _context;
        public SaleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Sale>> GetSales()
        {
            return await _context.Sales.Select(s => new Sale
            {
                Id = s.Id,
                UserName = s.UserName,
                Date = s.Date,
                SaleEntries = s.SaleEntries
            }).ToListAsync();
        }
    }
}
