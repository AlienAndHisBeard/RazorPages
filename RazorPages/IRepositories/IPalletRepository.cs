using DataModel;

namespace RazorPages.IRepositories
{
    public interface IPalletRepository
    {
        Task<List<Pallet>> GetPallets();

    }
}
