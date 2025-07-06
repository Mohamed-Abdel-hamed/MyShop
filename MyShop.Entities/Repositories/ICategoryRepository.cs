using MyShop.Entities.Models;

namespace MyShop.Entities.Repositories;
public  interface ICategoryRepository:IBaseRepository<Category>
{
    //custom logic
    void Edit(Category category);

}
