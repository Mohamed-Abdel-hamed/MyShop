using Microsoft.AspNetCore.Mvc;
using MyShop.Entities.Repositories;
using System.Security.Claims;

namespace MyShop.Web.ViewComponents;

public class ShoppingCartViewComponent(IUnitOfWork unitOfWork,IHttpContextAccessor httpContext) : ViewComponent
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IHttpContextAccessor _httpContext = httpContext;

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var claim= _httpContext.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier);
        if(claim !=null)
        {
            if(_httpContext.HttpContext.Session.GetInt32("CartCount")!=null)
            {
                return View(_httpContext.HttpContext.Session.GetInt32("CartCount"));
            }
            else
            {
                var totalCartCount = _unitOfWork.ShoppingCarts.GetAll(c => c.UserId == claim.Value).Count();
                _httpContext.HttpContext.Session.SetInt32("CartCount", totalCartCount);
                return View(totalCartCount);
            }
        }

        else
        {
            _httpContext.HttpContext.Session.Clear();
            return View(0);
        }
    }
}
