using Microsoft.EntityFrameworkCore;
using MyShop.Application.ViewModels;
using MyShop.Entities.Models;
using MyShop.Entities.Repositories;


namespace MyShop.Application.Services;
public class ProductService(IUnitOfWork unitOfWork) : IProductService
{
    private readonly IUnitOfWork _unitOfWork=unitOfWork;

    public IEnumerable<ProductViewModel> GetAll()
    {
        var products = _unitOfWork.Products.GetQueryable()
            .AsNoTracking()
            .Include(p => p.Category).Select(p => new ProductViewModel
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            ImageUrl = p.ImageUrl,
            CategoryName = p.Category.Name,
            CreateTime=p.CreateTime
        })
            .ToList();

        return products;
    }
    public IQueryable<Product> GetQueryable()
    {
        return _unitOfWork.Products.GetQueryable();
    }
    public object GetProducts(IQueryable<Product> products, int skip, int pageSize)
    {

        var mappingData = products
     .Select(p => new ProductViewModel
     {
         Id = p.Id,
         Name = p.Name,
         Description = p.Description,
         Price = p.Price,
         CategoryName = p.Category.Name,
         CreateTime = p.CreateTime
     })
     .Skip(skip)
     .Take(pageSize)
     .ToList();

        var recordTotal=products.Count();

        var jsonData = new { recordsFiltered = recordTotal, recordTotal, data = mappingData, };

        return jsonData;
    }
    public Product? GetById(int id)
    {
        var product=_unitOfWork.Products.GetById(id); 
        if (product is null)
            return null;
        return product;
    }
    public ProductViewModel? Details(int id)
    {
        var product = _unitOfWork.Products.Find(p => p.Id == id, includes: new[] { "Category" });
        if (product is null)
            return null;

        ProductViewModel productViewModel = new()
        {
            Id = id,
            Name = product!.Name,
            Description = product.Description,
            Price = product.Price,
            ImageUrl = product.ImageUrl,
            CategoryName= product.Category.Name,
        };
        return productViewModel;
    }
    public void Add(ProductFormModel model, string imageUrl)
    {

        Product product = new()
        {
            Name = model.Name,
            Description = model.Description,
            Price = model.Price,
            ImageUrl=imageUrl,
            CategoryId=model.SelectedCategory
        };
        _unitOfWork.Products.Add(product);
        _unitOfWork.Complete();
    }

    public void Edit(ProductFormModel model, string imageUrl)
    {
       var currentProduct=_unitOfWork.Products.GetById(model.Id);

        if (currentProduct is null) return;

        currentProduct.Name = model.Name;
        currentProduct.Description = model.Description;
        currentProduct.Price = model.Price;
        currentProduct.ImageUrl = imageUrl;
        currentProduct.CategoryId = model.SelectedCategory;
        _unitOfWork.Complete();
    }
    public void Delete(int id)
    {
        var currentProduct = _unitOfWork.Products.GetById(id);
        _unitOfWork.Products.Remove(currentProduct!);
        _unitOfWork.Complete();
    }
}
