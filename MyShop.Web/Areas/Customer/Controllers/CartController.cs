using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyShop.Application.Services;
using MyShop.Application.ViewModels;
using MyShop.Entities.Models;
using Stripe.Checkout;
using System.Security.Claims;

namespace MyShop.Web.Areas.Customer.Controllers;
[Area("Customer")]
public class CartController(IShoppingCartService shoppingCartService,
    IApplicationUserService applicationUserService,
    IOrderHeaderService orderHeaderService,
     IOrderDetailService orderDetailService) : Controller
{
    private readonly IShoppingCartService _shoppingCartService=shoppingCartService;
    private readonly IOrderHeaderService _orderHeaderService = orderHeaderService;
    private readonly IOrderDetailService _orderDetailService = orderDetailService;
    private readonly IApplicationUserService _applicationUserService = applicationUserService;
    [Authorize]
    public IActionResult Index()
    {
        var userId= User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        _shoppingCartService.GetCartItemsByUserId(userId);
        return View(_shoppingCartService.GetCartItemsByUserId(userId));
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public IActionResult AddToCart(int productId)
    {

        var userId=User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        _shoppingCartService.AddToCart(productId,userId);

        return RedirectToAction("Details", "Home", new { id =productId});
    }

    public IActionResult IncreaseQuantity(int cardId)
    {
        var cart=_shoppingCartService.GetById(cardId);
        if(cart is null)
            return NotFound();
        _shoppingCartService.IncreaseQuantity(cart,1);
        return RedirectToAction("Index");
    }

    public IActionResult DecreaseQuantity(int cardId)
    {
        var cart = _shoppingCartService.GetById(cardId);
        if (cart is null)
            return NotFound();
        _shoppingCartService.DecreaseQuantity(cart, 1);
        return RedirectToAction("Index");
    }
    [Authorize]
    public async Task<IActionResult> Summary()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var user = await _applicationUserService.GetById(userId);
        if (user is null)
            return NotFound();


        OrderHeaderFormModel ViewModel = new()
        {
            ShoppingCartItemsViewModel = _shoppingCartService.GetCartItemsByUserId(userId).ToList(),
            UsersViewModel = new ApplicationUserViewModel
            {
                Name = user.Name,
                Address = user.Address,
                City = user.City,
                PhoneNumber=user.PhoneNumber??string.Empty
            },
        };

        ViewModel.TotalPrice=ViewModel.ShoppingCartItemsViewModel.Sum(t=>t.TotalItemsPrice);

        return View(ViewModel);
    }
    [Authorize]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Summary(OrderHeaderFormModel model)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

        var user = await _applicationUserService.GetById(userId);

        if (user is null)

            return NotFound();

        model.ShoppingCartItemsViewModel= _shoppingCartService.GetCartItemsByUserId(userId);

        model.TotalPrice = model.ShoppingCartItemsViewModel.Sum(item => item.TotalItemsPrice);
        model.UsersViewModel = new ApplicationUserViewModel
        {
            Name = user.Name,
            Address = user.Address,
            City = user.City,
            PhoneNumber = user.PhoneNumber ?? string.Empty
        };

        if (!ModelState.IsValid)

        {
            return View(model);
        }

        OrderHeader orderHeader = _orderHeaderService.Add(userId,model);

        foreach (var cartItem in model.ShoppingCartItemsViewModel)
        {
            _orderDetailService.Add(cartItem.Product.Id, orderHeader.Id, cartItem.TotalItemsPrice, cartItem.Count);
        }
        var domain = "https://localhost:7086";
        var options = new SessionCreateOptions
        {
            LineItems = new List<SessionLineItemOptions>(),
            Mode = "payment",
            SuccessUrl = domain + $"/customer/cart/orderconfirmation?id={orderHeader.Id}",
            CancelUrl = domain + "/customer/cart/index",
        };


        foreach (var cartItem in model.ShoppingCartItemsViewModel)
        {
           var sessionLineItemOptions=  new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long)(cartItem.Product.Price * 100), 
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = cartItem.Product.Name,
                    }
                },
                Quantity = cartItem.Count
            };
            options.LineItems.Add(sessionLineItemOptions);
        }




        var service = new SessionService();

        Session session = service.Create(options);

        _orderHeaderService.UpdatePaymentInOrder(orderHeader.Id,session.Id);

        Response.Headers.Location = session.Url;
        return new StatusCodeResult(303);
    }
    [Authorize]
    public IActionResult OrderConfirmation(int id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var orderHeader = _orderHeaderService.GetById(id);
        if (orderHeader is null) 
            return NotFound();
        SessionService service = new();
        Session session=service.Get(orderHeader.SessionId);

       _orderHeaderService.UpdatePaymentIntentIdOrder(orderHeader.Id, session.PaymentIntentId);

        if (session.PaymentStatus.ToLower()=="paid")
        {
            _orderHeaderService.UpdateOrderStatus(orderHeader.Id, "Approve", "Approve",session.PaymentIntentId);

        }

        _shoppingCartService.RemoveRange(userId);
        return View(id);

    }


}
