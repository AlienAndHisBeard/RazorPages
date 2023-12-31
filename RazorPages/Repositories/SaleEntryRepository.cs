using DataModel;
using Microsoft.EntityFrameworkCore;
using RazorPages.Data;
using RazorPages.IRepositories;

namespace RazorPages.Repositories
{
    public class SaleEntryRepository : ISaleEntryRepository
    {
        private readonly ApplicationDbContext _context;
        public SaleEntryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<SaleEntry>> GetSaleEntries()
        {
            return await _context.SaleEntries.Select(s => new SaleEntry
            {
                Id = s.Id,
                Amount = s.Amount,
                Product = s.Product
            }).ToListAsync();
        }
    }
}
