
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MyShop.Entities.Models;

namespace MyShop.Application.ViewModels;
public class OrderHeaderFormModel
{
    public string? OrderStatus { get; set; }
    public decimal TotalPrice { get; set; }
    public string? PaymentStatus { get; set; }
    public string? TrackingNumber { get; set; }
    public string? Carrier { get; set; }
    public DateTime ShippingDate { get; set; }
    public string? SessionId { get; set; }
    public string? PaymentIntentId { get; set; }
    public ApplicationUserViewModel UsersViewModel { get; set; }
    [ValidateNever]
    public IEnumerable<ShoppingCartViewModel> ShoppingCartItemsViewModel { get; set; }
}
