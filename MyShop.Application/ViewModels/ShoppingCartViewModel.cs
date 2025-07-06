
using MyShop.Entities.Models;

namespace MyShop.Application.ViewModels;
public class ShoppingCartViewModel
{
    public int Id { get; set; }
    public ProductViewModel Product { get; set; }
    public int Count { get; set; }
    public decimal TotalItemsPrice => Count*Product.Price;
}
