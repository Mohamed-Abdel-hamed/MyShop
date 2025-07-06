using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyShop.Application.Services;

namespace MyShop.Web.Areas.Customer.Controllers;
[Area("Customer")]
public class HomeController(IProductService productService) : Controller
{
    private readonly IProductService _productService=productService;
    public IActionResult Index()
    {
        return View(_productService.GetAll());
    }
    public IActionResult Details(int id)
    {
        var shoppingCartViewModel = _productService.Details(id);

        if(shoppingCartViewModel == null) 

            return NotFound();

        return View(shoppingCartViewModel);
    }
}
