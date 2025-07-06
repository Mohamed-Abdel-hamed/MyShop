
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MyShop.Application.ViewModels;
public  class OrderViewModel
{
    public OrderHeaderViewModel OrderHeaderViewModel { get; set; }
    [ValidateNever]
    public IEnumerable<OrderDetailsViewModel> OrderDetailsViewModel { get; set; }
}
