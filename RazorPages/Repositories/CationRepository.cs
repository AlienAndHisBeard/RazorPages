using DataModel;
using Microsoft.EntityFrameworkCore;
using RazorPages.Data;
using RazorPages.IRepositories;

namespace RazorPages.Repositories
{
    public class CationRepository : ICationRepository
    {
        private readonly ApplicationDbContext _context;
        public CationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Cation>> GetCations()
        {
            return await _context.Cations.Select(c => new Cation
            {
                Id = c.Id,
                Name = c.Name,
                Symbol = c.Symbol,
                Concentration = c.Concentration
            }).ToListAsync();
        }
    }
}
