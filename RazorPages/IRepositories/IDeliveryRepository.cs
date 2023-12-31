using DataModel;

namespace RazorPages.IRepositories
{
    public interface IDeliveryRepository
    {
        Task<List<Delivery>> GetDeliveries();
    }
}
