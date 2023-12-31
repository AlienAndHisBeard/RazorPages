using DataModel;

namespace RazorPages.IRepositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts();
    }
}
