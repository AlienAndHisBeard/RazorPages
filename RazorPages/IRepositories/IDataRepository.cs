using DataModel;

namespace RazorPages.IRepositories
{
    public interface IDataRepository
    {
        IEnumerable<string> GetWaterTypes();
        List<Product> GetProducts();
        List<string> GetUserNames();

        Task AddSale(Sale sale);
    }
}
