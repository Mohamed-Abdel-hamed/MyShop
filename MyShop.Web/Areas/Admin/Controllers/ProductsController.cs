using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using MyShop.Application.Services;
using MyShop.Application.ViewModels;
using MyShop.Entities.Models;
using MyShop.Web.Services;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MyShop.Web.Areas.Admin.Controllers;
[Area("Admin")]
public class ProductsController(IProductService productService, ICategoryService categoryService, IImageService imageService) : Controller
{
    private readonly IImageService _imageService=imageService;
    private readonly ICategoryService _categoryService = categoryService;
    private readonly IProductService _productService=productService;
    public IActionResult Index()
    {
        return View(_productService.GetAll());
    }

    [HttpPost]
    public IActionResult GetProducts()
    {
        var skip = int.Parse(Request.Form["start"]!);
        var pageSize = int.Parse(Request.Form["length"]!);
        var sortColumnIndex = Request.Form["order[0][column]"]!;
        var sortColumn = Request.Form[$"columns[{sortColumnIndex}][name]"]!;
        var sortColumnDirection = Request.Form["order[0][dir]"];
        var searchValue = Request.Form["search[value]"];

        IQueryable<Product> products = _productService.GetQueryable();
        if (!string.IsNullOrEmpty(searchValue))
        {
            products = products.Where(c =>
                c.Name.Contains(searchValue!) ||
                c.Category.Name.Contains(searchValue!));
        }
        products = products.OrderBy($"{sortColumn} {sortColumnDirection}");

        return Ok(_productService.GetProducts(products,skip,pageSize));
    }
    public IActionResult Create()
    {
        var categories=_categoryService.selectListsCategories();

        ProductFormModel model = new()
        {
            Categories = categories,
        };
        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> Create(ProductFormModel model)
    {
        if(!ModelState.IsValid)
        {
            model.Categories = _categoryService.selectListsCategories();
            return View(model);
        }

        if(model.Image is  null)
        {
            ModelState.AddModelError(nameof(Image),"Image is required");
            model.Categories = _categoryService.selectListsCategories();
            return View(model);
        }
        var imageName = $"{Guid.NewGuid()}{Path.GetExtension(model.Image.FileName)}";
        var imagePath = "/images/products";

        var (isUploaded, errorMessage) = await _imageService.UploadImageAsync(model.Image, imageName, imagePath);
        if(!isUploaded)
        {
            ModelState.AddModelError(nameof(Image), errorMessage!);
            model.Categories = _categoryService.selectListsCategories();
            return View(model);
        }
        
        _productService.Add(model, $"{imagePath}/{imageName}");

        TempData["create"] = "item has created successfully";

        return RedirectToAction(nameof(Index));
    }
    public IActionResult Edit(int id)
    {
        var product=_productService.GetById(id);

        if(product is null)

            return NotFound();

        var categories = _categoryService.selectListsCategories();

        ProductFormModel model = new()
        {
            Id = id,
            Name=product.Name,
            Description=product.Description,
            Price=product.Price,
            ImageUrl=product.ImageUrl,
            SelectedCategory=product.CategoryId,
            Categories = categories,
        };

        return View(model);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(ProductFormModel model)
    {
        var product = _productService.GetById(model.Id);

        if (product is null)

            return NotFound();

        if (!ModelState.IsValid)
        {
            model.Categories = _categoryService.selectListsCategories();

            model.SelectedCategory = product.CategoryId;

            return View(model);
        }

            if (model.Image is null)
            {
                model.ImageUrl = product.ImageUrl;
           
                _productService.Edit(model, product.ImageUrl); 
                
                return RedirectToAction(nameof(Index));
            }
            var (isDelete, deleteMessage) =_imageService.DeleteImage(product.ImageUrl);

            if (!isDelete)
            {
                 model.Categories = _categoryService.selectListsCategories();
               
                 model.SelectedCategory = product.CategoryId;
               
                 return View(model);
            }
            var imageName = $"{Guid.NewGuid()}{Path.GetExtension(model.Image.FileName)}";

            var imagePath = "/images/products";

            var (isUploaded, errorMessage) = await _imageService.UploadImageAsync(model.Image, imageName, imagePath);

            if (!isUploaded)
            {
                ModelState.AddModelError(nameof(Image), errorMessage!);

                model.Categories = _categoryService.selectListsCategories();

                model.SelectedCategory = product.CategoryId;

                model.ImageUrl = product.ImageUrl;

                return View(model);
            }
            _productService.Edit(model, $"{imagePath}/{imageName}");

        TempData["update"] = "item has updated successfully";

        return RedirectToAction(nameof(Index));
    }
    [HttpPost]
    public IActionResult Delete(int id)
    {
        var product = _productService.GetById(id);

        if (product is null)

            return NotFound();

        _productService.Delete(id);

        TempData["delete"] = "item has deleted successfully";

        return Ok();
    }
}
