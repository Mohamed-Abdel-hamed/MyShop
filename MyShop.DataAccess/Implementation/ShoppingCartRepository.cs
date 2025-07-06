using MyShop.DataAccess.Data;
using MyShop.Entities.Models;
using MyShop.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.Implementation;
public class ShoppingCartRepository(ApplicationDbContext context) : BaseRepository<ShoppingCart>(context), IShoppingCartRepository
{
    public int DecreaseQuantity(ShoppingCart cart, int count)
    {
        cart.Count -= count;
        return cart.Count;
    }

    public int IncreaseQuantity(ShoppingCart cart, int count)
    {
        cart.Count += count;
        return cart.Count;
    }
}
