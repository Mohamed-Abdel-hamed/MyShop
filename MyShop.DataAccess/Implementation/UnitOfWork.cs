using MyShop.DataAccess.Data;
using MyShop.Entities.Models;
using MyShop.Entities.Repositories;

namespace MyShop.DataAccess.Implementation;
public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    private readonly ApplicationDbContext _context=context;
    public ICategoryRepository Categories => new CategoryRepository(_context);

    public IBaseRepository<Product> Products => new BaseRepository<Product>(_context);

    public IShoppingCartRepository ShoppingCarts =>  new ShoppingCartRepository(_context);

    public IBaseRepository<OrderHeader> OrderHeaders => new BaseRepository<OrderHeader>(_context);

    public IBaseRepository<OrderDetail> OrderDetails => new BaseRepository<OrderDetail>(_context);

    public IBaseRepository<ApplicationUser> Users => new BaseRepository<ApplicationUser>(_context);

    public int Complete()
    {
      return _context.SaveChanges();
    }
}
