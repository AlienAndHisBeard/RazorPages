using DataModel;
using RazorPages.Data;
using RazorPages.IRepositories;

namespace RazorPages.Repositories
{
    public class DataRepository : IDataRepository
    {
        private readonly ApplicationDbContext _context;

        public DataRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<string> GetWaterTypes()
        {
            var waterTypes = Enum.GetValues(typeof(DrinkTypes));
            foreach (var waterType in waterTypes)
            {
                yield return waterType.ToString();
            }
        }

        public List<Product> GetProducts() => _context.Products.ToList();

        public List<string> GetUserNames() => _context.Users.Select(p => p.UserName).ToList();

        public async Task AddSale(Sale sale)
        {
            foreach (var saleEntry in sale.SaleEntries)
            {
                if (saleEntry.Amount > _context.Products.Find(saleEntry.Product.Id).AvailableAmount) return;
            }
            foreach (var saleEntry in sale.SaleEntries)
            {
                _context.Products.Find(saleEntry.Product.Id).AvailableAmount -= saleEntry.Amount;
            }
            _context.Sales.Add(sale);
            _ = await _context.SaveChangesAsync();
        }
    }
}
