using MASHROEE.Models;
using MASHROEE.Repository;

namespace MASHROEE.IRepository
{
    public interface ICartRepository
    {
       Task<List<Cart>> GetAllCarts();

		Task AddToCartAsync(string userId, int productId, int quantity);
        Task RemoveFromCartAsync(string userId, int productId);
        Task UpdateCartItemQuantityAsync(string userId, int productId, int quantity);
        Task<List<Cartitem>> GetCartItemsAsync(string userId);
        Task ClearCartAsync(string userId);
        Task<decimal> GetCartTotalAsync(string userId);
    }
}
