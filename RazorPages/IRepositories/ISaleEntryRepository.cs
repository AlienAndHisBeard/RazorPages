using DataModel;

namespace RazorPages.IRepositories
{
    public interface ISaleEntryRepository
    {
        Task<List<SaleEntry>> GetSaleEntries();

    }
}
