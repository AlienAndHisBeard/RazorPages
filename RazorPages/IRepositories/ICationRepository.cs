using DataModel;

namespace RazorPages.IRepositories
{
    public interface ICationRepository
    {
        Task<List<Cation>> GetCations();
    }
}
