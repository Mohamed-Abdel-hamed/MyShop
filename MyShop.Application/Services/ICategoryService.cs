
using Microsoft.AspNetCore.Mvc.Rendering;
using MyShop.Application.ViewModels;
using MyShop.Entities.Models;

namespace MyShop.Application.Services;
public interface ICategoryService
{
    IEnumerable<CategoryViewModel> GetAll();
    IEnumerable<SelectListItem> selectListsCategories();
    Category? GetById(int id);
    void Add(CategoryFormModel model);
    void Edit(CategoryFormModel model);
    void Delete(int id);
    bool AllowItem(int id, string name);
}
