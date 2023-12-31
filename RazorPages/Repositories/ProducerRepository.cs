using DataModel;
using Microsoft.EntityFrameworkCore;
using RazorPages.Data;
using RazorPages.IRepositories;

namespace RazorPages.Repositories
{
    public class ProducerRepository : IProducerRepository
    {
        private readonly ApplicationDbContext _context;
        public ProducerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Producer>> GetProducers()
        {
            return await _context.Producers.Select(p => new Producer
            {
                Id = p.Id,
                Name = p.Name,
                Email = p.Email,
                PhoneNumber = p.PhoneNumber
            }).ToListAsync();
        }
    }
}
