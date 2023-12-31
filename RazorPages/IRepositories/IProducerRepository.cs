using DataModel;

namespace RazorPages.IRepositories
{
    public interface IProducerRepository
    {
        Task<List<Producer>> GetProducers();
    }
}
