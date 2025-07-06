
using Microsoft.AspNetCore.Http;
using MyShop.Application.ViewModels;
using MyShop.Entities.Models;
using MyShop.Entities.Repositories;
namespace MyShop.Application.Services;
public class ShoppingCartService(IUnitOfWork unitOfWork,IHttpContextAccessor httpContext) : IShoppingCartService
{
    private readonly IUnitOfWork _unitOfWork=unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor=httpContext;
    public IEnumerable<ShoppingCartViewModel> GetCartItemsByUserId(string userId)
    {
        var cartItems = _unitOfWork.ShoppingCarts.GetAll(c => c.UserId == userId, ["Product.Category"])
            .Select(c=>new ShoppingCartViewModel
            {
                Id = c.Id,
                Product=new ProductViewModel
                {
                    Id = c.Product.Id,
                    Name = c.Product.Name,
                    Price = c.Product.Price,
                    Description = c.Product.Description,
                    ImageUrl = c.Product.ImageUrl,
                    CategoryName = c.Product.Category.Name,
                },
                Count=c.Count
            });
        return cartItems;

    }
    public ShoppingCart? GetById(int id)
    {
        var cart=_unitOfWork.ShoppingCarts.GetById(id);
        if (cart == null)
            return null;
        return cart;
    }
    public void AddToCart(int productId,string userId, int count = 1)
    {
        var currentCartItem = _unitOfWork.ShoppingCarts
      .Find(c => c.UserId == userId && c.ProductId == productId);

        if (currentCartItem is not null) 
        {
            IncreaseQuantity(currentCartItem,count);
        }
        else
        {
            var newCartItem = new ShoppingCart
            {
                ProductId = productId,
                UserId = userId,
                Count = count
            };

            _unitOfWork.ShoppingCarts.Add(newCartItem);
            _unitOfWork.Complete();
        }
        var totalCartCount = _unitOfWork.ShoppingCarts.GetAll(c => c.UserId == userId).Count();
        _httpContextAccessor.HttpContext.Session.SetInt32("CartCount", totalCartCount);

    }
    public int DecreaseQuantity(ShoppingCart cart, int count)
    {
       var newCount= _unitOfWork.ShoppingCarts.DecreaseQuantity(cart, count);
        if (newCount < 1)
        {
            _unitOfWork.ShoppingCarts.Remove(cart);
            _unitOfWork.Complete();
        }
        _unitOfWork.Complete();
        var totalCartCount = _unitOfWork.ShoppingCarts.GetAll(c => c.UserId == cart.UserId).Count();
        _httpContextAccessor.HttpContext.Session.SetInt32("CartCount", totalCartCount);
        return newCount;
    }
    public int IncreaseQuantity(ShoppingCart cart, int count)
    {
        var newCount = _unitOfWork.ShoppingCarts.IncreaseQuantity(cart, count);
        _unitOfWork.Complete();
        return newCount;
    }
    public void RemoveRange(string userId)
    {
        var cartItems = _unitOfWork.ShoppingCarts.GetAll(c => c.UserId == userId, ["Product.Category"]);
        _unitOfWork.ShoppingCarts.RemoveRange(cartItems);
        _unitOfWork.Complete();
        var totalCartCount = _unitOfWork.ShoppingCarts.GetAll(c => c.UserId == userId).Count();
        _httpContextAccessor.HttpContext.Session.SetInt32("CartCount", totalCartCount);
    }
}
