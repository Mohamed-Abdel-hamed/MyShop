
using MyShop.Application.ViewModels;
using MyShop.Entities.Models;

namespace MyShop.Application.Services;
public interface IShoppingCartService
{
    IEnumerable<ShoppingCartViewModel> GetCartItemsByUserId(string userId);
    ShoppingCart? GetById(int id);
    void AddToCart(int productId, string userId, int count = 1);
    int IncreaseQuantity(ShoppingCart cart, int count);
    int DecreaseQuantity(ShoppingCart cart, int count);
    void RemoveRange(string userId);
}
