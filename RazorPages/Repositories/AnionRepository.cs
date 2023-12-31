using DataModel;
using Microsoft.EntityFrameworkCore;
using RazorPages.Data;
using RazorPages.IRepositories;

namespace RazorPages.Repositories
{
    public class AnionRepository : IAnionRepository
    {
        private readonly ApplicationDbContext _context;

        public AnionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Anion>> GetAnions()
        {
            return await _context.Anions.Select(a => new Anion
            {
                Id = a.Id,
                Name = a.Name,
                Symbol = a.Symbol,
                Concentration = a.Concentration,
            }).ToListAsync();
        }
    }
}
