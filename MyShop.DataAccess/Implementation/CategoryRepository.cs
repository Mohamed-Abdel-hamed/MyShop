using MyShop.DataAccess.Data;
using MyShop.Entities.Models;
using MyShop.Entities.Repositories;

namespace MyShop.DataAccess.Implementation;
internal class CategoryRepository(ApplicationDbContext context) : BaseRepository<Category>(context), ICategoryRepository
{
    private readonly ApplicationDbContext _context = context;
   public void Edit(Category category)
    {
        var currentCategory = _context.Categories.FirstOrDefault(c => c.Id == category.Id);
        if (currentCategory != null)
        {
            currentCategory.Name = category.Name;
            currentCategory.Description = category.Description;
        }
    }
}
