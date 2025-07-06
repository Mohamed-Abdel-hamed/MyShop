using MyShop.Application.ViewModels;
using MyShop.Entities.Models; 
namespace MyShop.Application.Services;
public interface IProductService
{
    IEnumerable<ProductViewModel> GetAll();
    object GetProducts(IQueryable<Product> products,int skip,int pageSize);
    IQueryable<Product> GetQueryable();
    Product? GetById(int id);
    ProductViewModel? Details(int id);
    void Add(ProductFormModel model, string imageUrl);
    void Edit(ProductFormModel model, string imageUrl);
    void Delete(int id);
}
