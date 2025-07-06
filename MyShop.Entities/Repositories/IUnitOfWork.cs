
using MyShop.Entities.Models;

namespace MyShop.Entities.Repositories;
public interface IUnitOfWork
{
    ICategoryRepository Categories { get; }
    IBaseRepository<Product> Products { get; }
    IShoppingCartRepository ShoppingCarts { get; }
    IBaseRepository<OrderHeader> OrderHeaders { get; }
    IBaseRepository<OrderDetail> OrderDetails { get; }
    IBaseRepository<ApplicationUser> Users { get; }
    int Complete();
}
