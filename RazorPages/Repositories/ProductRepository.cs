using DataModel;
using Microsoft.EntityFrameworkCore;
using RazorPages.Data;
using RazorPages.IRepositories;

namespace RazorPages.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _context.Products.Select(p => new Product
            {
                Id = p.Id,
                Name = p.Name,
                Type = p.Type,
                Producer = p.Producer,
                Ph = p.Ph,
                PackingType = p.PackingType,
                Volume = p.Volume
            }).ToListAsync();
        }
    }
}
