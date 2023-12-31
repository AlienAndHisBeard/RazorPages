using DataModel;
using Microsoft.EntityFrameworkCore;
using RazorPages.Data;
using RazorPages.IRepositories;

namespace RazorPages.Repositories
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly ApplicationDbContext _context;
        public DeliveryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Delivery>> GetDeliveries()
        {
            return await _context.Deliveries.Select(d => new Delivery
            {
                Id = d.Id,
                UserName = d.UserName,
                Date = d.Date,
                Supplier = d.Supplier,
                Pallets = d.Pallets
            }).ToListAsync();
        }
    }
}
