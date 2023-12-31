using DataModel;

namespace RazorPages.IRepositories
{
    public interface IAnionRepository
    {
        Task<List<Anion>> GetAnions();
    }
}
