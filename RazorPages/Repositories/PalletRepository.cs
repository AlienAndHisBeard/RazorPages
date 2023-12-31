using DataModel;
using Microsoft.EntityFrameworkCore;
using RazorPages.Data;
using RazorPages.IRepositories;

namespace RazorPages.Repositories
{
    public class PalletRepository : IPalletRepository
    {
        private readonly ApplicationDbContext _context;
        public PalletRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Pallet>> GetPallets()
        {
            return await _context.Pallets.Select(p => new Pallet
            {
                Id = p.Id,
                Amount = p.Amount,
                Product = p.Product
            }).ToListAsync();
        }
    }
}
