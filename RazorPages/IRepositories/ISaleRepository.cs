using DataModel;

namespace RazorPages.IRepositories
{
    public interface ISaleRepository
    {
        Task<List<Sale>> GetSales();
    }
}
