
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyShop.Application.ViewModels;
using MyShop.DataAccess.Implementation;
using MyShop.Entities.Models;
using MyShop.Entities.Repositories;
using System.Reflection.Metadata.Ecma335;
namespace MyShop.Application.Services;
 public class CategoryService(IUnitOfWork unitOfWork) : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork=unitOfWork;
    public IEnumerable<CategoryViewModel> GetAll()
    {
       var categories = _unitOfWork.Categories.GetAll(null);

     var viewModel=categories.Select(c=> new CategoryViewModel
     {

         Id = c.Id,
         Name = c.Name,
         Description = c.Description,
         CreatedDate = c.CreatedDate,
     })
       .ToList();
        return viewModel;
    }
    public IEnumerable<SelectListItem> selectListsCategories()
    {
        var Categories = _unitOfWork.Categories.GetQueryable().AsNoTracking();
        return Categories.Select(c => new SelectListItem
        {
            Value = c.Id.ToString(),
            Text = c.Name,
        })
          .ToList();
    }
    public Category? GetById(int id)
    {
        var category = _unitOfWork.Categories.GetById(id);

        if (category is null) 

            return null;

        return category;
    }
    public void Add(CategoryFormModel model)
    {
        Category category = new()
        {
            Name = model.Name,
            Description = model.Description,
        };
        _unitOfWork.Categories.Add(category);

        _unitOfWork.Complete();
    }

    public void Edit(CategoryFormModel model)
    {
        var currentCategory=_unitOfWork.Categories.GetById(model.Id);

        currentCategory!.Name = model.Name;

        currentCategory.Description = model.Description;

        _unitOfWork.Categories.Update(currentCategory);

        _unitOfWork.Complete();

    }

    public void Delete(int id)
    {
        var currentCategory = _unitOfWork.Categories.GetById(id);

        _unitOfWork.Categories.Remove(currentCategory!);

        _unitOfWork.Complete();
    }
    public bool AllowItem(int id,string name)
    {
        var category = _unitOfWork.Categories.Find(c=>c.Name== name);
        var isAllow = category is null ||category.Id== id;
           
            return isAllow;
    }
}
