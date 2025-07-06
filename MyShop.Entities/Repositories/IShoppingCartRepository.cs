using MyShop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Entities.Repositories;
public interface IShoppingCartRepository : IBaseRepository<ShoppingCart>
{
    int IncreaseQuantity(ShoppingCart cart,int count);
    int DecreaseQuantity(ShoppingCart cart, int count);
}
